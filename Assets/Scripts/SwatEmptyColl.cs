using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatEmptyColl : MonoBehaviour
{
    // Start is called before the first frame update
    Enemy enemy;
    Limb limb;
    private float boundz=-15, boundx=-15,boundy=-1;
    public float speed;
   
    private Collider[] childrenColl;
    
    private Limb[] childLimb;
    void Start()
    {
        limb=GetComponentInChildren<Limb>();
        enemy=GetComponentInChildren<Enemy>();
        childrenColl=GetComponentsInChildren<Collider>();
        childLimb=GetComponentsInChildren<Limb>();
        
       
        foreach(Collider col in childrenColl){
            if(col !=GetComponent<Collider>()){
                Physics.IgnoreCollision(GetComponent<Collider>(),col);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back*3*Time.deltaTime);
         CheckBounds();
        
        
    }
    private void OnCollisionEnter(Collision other) {
        
        if(other.gameObject.CompareTag("Egg")){
           FindObjectOfType<GameManager>().UpdateScore(5);
            foreach(Limb lb in childLimb){
               if(!limb.isExploded) limb.GetHit();
                
                 limb.isExploded=true;
            }
            
        }
        if(!other.gameObject.CompareTag("Ground")&& !enemy.isKilled && !other.gameObject.CompareTag("Obstacle")){
            enemy.isKilled=true;
            enemy.GetKilled();
            
        } 
        
    }
    private void OnTriggerEnter(Collider other) {
        Destroy(this.gameObject);
    }
     void CheckBounds(){
        if(transform.position.z<boundz)
            gameObject.SetActive(false);
            //Destroy(this.gameObject);   
        
        if(transform.position.x<boundx)
             gameObject.SetActive(false);
            //Destroy(this.gameObject); 

        if(transform.position.y<boundy)
             gameObject.SetActive(false);
            //Destroy(this.gameObject);
    }
}
