using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float boundz=-15;
    private float boundx=-15;
    public float speed=10f;
    public bool isBullet;
    
    private Rigidbody enemyRb;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z<boundz)
            Destroy(this.gameObject);   
        
        if(transform.position.x<boundx)
            Destroy(this.gameObject);   
        
        if(!isBullet) transform.Translate(Vector3.back*speed*Time.deltaTime);
        
        
        
    }
}
