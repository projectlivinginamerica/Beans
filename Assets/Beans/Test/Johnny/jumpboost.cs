using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class jumpboost : MonoBehaviour
{
    //charges is how many uses it has, launch force is how strong the boost is
    [SerializeField] private int charges;
    [SerializeField] private float LaunchForce = 100f;
    
    void OnTriggerEnter(Collider Collision)
    {
      
        //get cart, check to see if there is one first
        ArcadeKart theCart = Collision.gameObject.GetComponent<ArcadeKart>();
        if (theCart == null)
            return;

        //get rigid body if and apply force
        Rigidbody rb = theCart.GetComponent<Rigidbody>();
        rb.AddForce(theCart.transform.up * LaunchForce);    

        charges  --;

        if (charges <= 0)
        {
        Destroy(gameObject);
        }
    }

  
}
