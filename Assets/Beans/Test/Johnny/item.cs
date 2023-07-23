using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    [SerializeField] private int charges;

    struct boostTarget
    {
        public float ContactTime;
    }

    [SerializeField] float BoostDelay = 0.1f;
    [SerializeField] float LaunchForce = 100f;

    Dictionary<Rigidbody, boostTarget> Targets = new Dictionary<Rigidbody, boostTarget>();
    List<Rigidbody> targetsToClear = new List<Rigidbody>();
    
    private void FixedUpdate()
    {          
        // check for targets
        
        float thresholdTime = Time.timeSinceLevelLoad - BoostDelay;
        
        foreach(var kvp in Targets)
        {
            if (kvp.Value.ContactTime >= thresholdTime)
            {
                Launch(kvp.Key);
            }
        }

        //clear targets    

        foreach(var target in targetsToClear)
            Targets.Remove(target);
        targetsToClear.Clear();
    }

    void OnTriggerEnter(Collider Collision)
    {
        Debug.Log($"{Collision.gameObject.name}");
        
        //attempt to get rigid body
        Rigidbody rb;
        if (Collision.gameObject.TryGetComponent<Rigidbody>(out rb))
        {
            Targets[rb] = new boostTarget() { ContactTime = Time.timeSinceLevelLoad};
        }

        charges  --;


        if (charges <= 0)
        {
        Destroy(gameObject);
        }
    }

    void Launch(Rigidbody targetRB)
    {
        targetRB.AddForce(transform.up * LaunchForce);
    }
}
