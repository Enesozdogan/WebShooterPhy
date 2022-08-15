using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    Enemy enemyScript;
    [SerializeField] Limb[] childLimbs;
    [SerializeField] GameObject limbPrefab;
    [SerializeField] GameObject bloodPrefab;
    public bool isExploded;
    // Start is called before the first frame update
    void Start()
    {
        enemyScript=GetComponentInParent<Enemy>();
        isExploded=false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetHit(){
      if(this!=null){
         if(childLimbs.Length>0){
            foreach(Limb limb in childLimbs){
                if(limb!=null) limb.GetHit();
            }

        }
        if(bloodPrefab!=null){
            var bloodIns=Instantiate(bloodPrefab,transform.position,transform.rotation);
            Destroy(bloodIns,1);
        
        }   

        if(limbPrefab!=null){
            var limbIns=Instantiate(limbPrefab,transform.position,transform.rotation);
            Destroy(limbIns,1);
        }

        transform.localScale=Vector3.zero;
        //enemyScript.GetKilled();
        
        Destroy(this);
      }  
       
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Egg"))
        GetHit();
    }
   
}
