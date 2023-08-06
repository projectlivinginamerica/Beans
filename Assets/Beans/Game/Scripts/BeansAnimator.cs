using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeansAnimator : MonoBehaviour
{
    [SerializeField] private Vector3 MovementVec;


    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.position += MovementVec * Time.fixedDeltaTime;
    }
}
