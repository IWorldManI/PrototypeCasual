using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAway : MonoBehaviour
{
    public NavMeshAgent self;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            self.SetDestination(-other.transform.position);
        }
    }
}
