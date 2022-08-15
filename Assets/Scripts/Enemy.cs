using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public bool isKilled=false;
    private Animator enemyAnim;
    
    private float boundz=-15, boundx=-15;
    
    
    List<Rigidbody> ragdollRigids;
    // Start is called before the first frame update
    void Start()
    {
        transform.position=transform.root.position+new Vector3(0,-0.55f,0);
        enemyAnim=GetComponent<Animator>();
        ragdollRigids=new List<Rigidbody>(transform.GetComponentsInChildren<Rigidbody>());
        ragdollRigids.Remove(GetComponent<Rigidbody>());
        DeactivateRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        
      
    }

    void ActivateRagdoll(){
        if(enemyAnim!=null)  enemyAnim.enabled=false;
       
        for(int i=0;i<ragdollRigids.Count;i++){
            ragdollRigids[i].useGravity=true;
            ragdollRigids[i].isKinematic=false;
        }
    }
    public void DeactivateRagdoll(){
        if(enemyAnim!=null)  enemyAnim.enabled=true;
        if(ragdollRigids!=null){
             for(int i=0;i<ragdollRigids.Count;i++){
            ragdollRigids[i].useGravity=false;
            ragdollRigids[i].isKinematic=true;
        }
        }
       
    }
    public void GetKilled(){
        ActivateRagdoll();
        //gameObject.SetActive(false);
        //Destroy(this.gameObject,1);
    }
      void CheckBounds(){
        if(transform.position.z<boundz)
            Destroy(this.gameObject);   
        
        if(transform.position.x<boundx)
            Destroy(this.gameObject); 
    }
    
   
}
