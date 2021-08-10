using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClosestGround : MonoBehaviour
{
    public Transform ground;
    private Collider groundCollider;

    private void Start()
    {
        groundCollider = ground.GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            Vector3 closestGround = groundCollider.ClosestPointOnBounds(transform.position);
            transform.position = new Vector3(closestGround.x, closestGround.y+10, closestGround.z);

        }

    }
   
}
