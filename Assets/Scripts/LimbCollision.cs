using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbCollision : MonoBehaviour
{
    // Start is called before the first frame update
    Limb limb;
    private Rigidbody rb;
    void Start()
    {
        limb=GetComponent<Limb>();
        rb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Egg")){
                limb.GetHit();
        }
    }
}
