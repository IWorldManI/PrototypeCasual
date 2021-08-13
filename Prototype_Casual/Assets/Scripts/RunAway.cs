using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAway : MonoBehaviour
{
    public NavMeshAgent self;
    public float distanceToRun;
    private void Start()
    {
        self = GetComponent<NavMeshAgent>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance < distanceToRun)
            {
                Vector3 otherTransform = other.transform.position;
                StartCoroutine(MovingAway(otherTransform));
                //Debug.Log(otherTransform);
                
            }
        }
    }
    IEnumerator MovingAway(Vector3 otherTransform)
    {
        yield return new WaitForSeconds(.3f);
        Vector3 dirToPlayer = transform.position - otherTransform;
        Vector3 newPos = transform.position + dirToPlayer;
        self.SetDestination(newPos);
    }
}
