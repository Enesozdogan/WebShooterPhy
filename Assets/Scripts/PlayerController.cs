using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed=10f;
    public int lives=4;
    public GameObject hitPoint;
    public bool canMove=true;
    public bool canRotate=true;
    public GameObject spiderEgg;
    public Vector3 mouseWorldPosition;
    public Transform eggPosition;
    
    public GameObject mouth;
    
    public Transform player;
   
    private Rigidbody playerRb;
    private LineRenderer lr;
    private Animator playerAnim;
    
    private float nextAttack=0f;
    private float fireRate=1f;
    private GameManager gameManager;
    private SoundManager soundManager;
    
    // Start is called before the first frame update
    void Awake(){
        lr=GetComponent<LineRenderer>();
    }
    void Start()
    {
        //Cursor.visible=false;
        gameManager=GameObject.Find("Game Manager").GetComponent<GameManager>();
        soundManager=GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        playerRb=GetComponent<Rigidbody>();
        playerAnim=transform.Find("spider").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
     move();   
     
     if(Time.time>=nextAttack){
        if(Input.GetKeyDown(KeyCode.Space)){
            Fire();
            nextAttack=Time.time+fireRate;
        }
     }

    }
    
    void move(){
      
        mouseWorldPosition=Camera.main.ScreenToWorldPoint(Input.mousePosition+new Vector3(0,0,(Camera.main.transform.position.y-transform.position.y)));
        hitPoint.transform.position=mouseWorldPosition;
        Debug.DrawLine(Camera.main.transform.position,mouseWorldPosition);
        
        if((canMove && !gameManager.isGameOver)){
            
            //transform.LookAt(mouseWorldPosition);
           
               Look(mouseWorldPosition);
               
           
          
              
          
            
            
            if(Input.GetKeyDown(KeyCode.Mouse1)){
                canMove=false;
                playerAnim.SetBool("IsMoving",true);
                
                // playerRb.AddForce(transform.forward*speed,ForceMode.Impulse);
                StartCoroutine(SetDestination(mouseWorldPosition));
                
            }
        }
        
    }



    void Fire(){
        Instantiate(spiderEgg,eggPosition.position,spiderEgg.transform.rotation);
    }
    void DrowWeb(Vector3 target){
        lr.SetPosition(0,mouth.transform.position);
        lr.SetPosition(1,target);
    }
    void Look(Vector3 vec1){
        float damping=20000f;
        var rotation=Quaternion.LookRotation(vec1-transform.position);
        transform.rotation=Quaternion.Slerp(transform.rotation,rotation,Time.deltaTime*damping);
        //playerRb.MoveRotation(rotation);
       
    }
     private bool IsFacingObject(){
        // Check if the gaze is looking at the front side of the object
         Vector3 forward = transform.forward;
          Vector3 toOther = (hitPoint.transform.position - transform.position).normalized;
 
        if(Vector3.Dot(forward, toOther) < 0.7f){
        
        return false;
        }
        
        return true;
    }
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Obstacle")){
            if(gameManager.lives>1) gameManager.UpdateLives(-1);
            else if(gameManager.lives<=1){
                gameManager.livesText.text="Lives: "+0;
                gameManager.GameOver();
                canMove=false;
                playerAnim.SetBool("IsDead",true);
                soundManager.DieSound();
                Destroy(this.gameObject,1.5f);      
            }
           
        }
    }
    


  
   
  
    IEnumerator SetDestination(Vector3 target){
        
        while(Vector3.Distance(transform.position,target)>0.3f && IsFacingObject()  ){
            
                
                playerRb.AddForce(transform.forward*speed*Time.deltaTime);
                DrowWeb(target);
                yield return null;
        }
         playerAnim.SetBool("IsMoving",false);       
        canMove=true;  
    }
    
   
   

}
