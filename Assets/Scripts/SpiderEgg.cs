using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEgg : MonoBehaviour
{
    public bool isDestroyed=false;
    [SerializeField] private float _triggerForce=0.5f;
    [SerializeField] private float _expolosionRadius=5f;
    [SerializeField] private float _explosionForce=500;
    [SerializeField] private GameObject explosionEffect;
    SoundManager soundManager;
    
    [SerializeField]    private ParticleSystem shockPart;
    private float startTime,killTime=2f;
    
    
    
    
    //[SerializeField] private GameObject _particles;
    void Start(){
        soundManager=GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        startTime=Time.time;
        Physics.IgnoreCollision(GameObject.Find("Player").GetComponent<SphereCollider>(),GetComponent<SphereCollider>());
        
        shockPart=GameObject.Find("Light Particle").GetComponent<ParticleSystem>();
        shockPart.transform.position=transform.position;
         shockPart.Play();
        //shockWave.transform.position=transform.position;
        
        
        explosionEffect.transform.position=transform.position;
    }
    void Update(){
         

        if((Time.time-startTime)>=killTime && !isDestroyed){
            isDestroyed=true;
           var exp=Instantiate(explosionEffect,transform.position,Quaternion.identity);
           Destroy(exp,0.8f);
           shockPart.Stop();
          
            Destroy(gameObject);
            
        }
        
    }
    
    private void OnCollisionEnter(Collision other) {
      
           
        if(other.relativeVelocity.magnitude>=_triggerForce){
            var surroundingObjects=Physics.OverlapSphere(transform.position,_expolosionRadius);

            foreach(var obj in surroundingObjects){
                var rb=obj.GetComponent<Rigidbody>();
                if(rb==null) continue;
                rb.AddExplosionForce(_explosionForce,transform.position,_expolosionRadius);
                
                
                
            }
            
             var exp=Instantiate(explosionEffect,transform.position,Quaternion.identity);
            Destroy(exp,1);
            soundManager.OnClick();
            if(shockPart.isPlaying)
            shockPart.Stop();
            Destroy(gameObject);
           
        }
    }
    
    
   
}
