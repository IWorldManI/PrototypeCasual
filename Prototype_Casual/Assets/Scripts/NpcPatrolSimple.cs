using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcPatrolSimple : MonoBehaviour
{
    [SerializeField] bool _patrolWaiting;
    [SerializeField] float _totalWaitTime = 3f;
    [SerializeField] float _switchProbability = 0.2f;
    [SerializeField] Collider sphereCollider;
    [SerializeField] List<GameObject> _patrolPoints;
    [SerializeField] List<GameObject> collidedObj = new List<GameObject>();
    [SerializeField] public int npcTrash;
    NavMeshAgent _navMeshAgent;
    int _currentPatrolIndex;
    bool _travaling;
    public bool _waiting;
    bool _patrolForward;
    public float _waitTimer;
    
    private void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        sphereCollider = this.GetComponent<SphereCollider>();

        if (_navMeshAgent==null)
        {
            Debug.Log("NavMeshAgent is not attached to" + gameObject.name);
        }
        else
        {
            if (_patrolPoints != null && _patrolPoints.Count >= 2)
            {
                _currentPatrolIndex = 0;
                SetDestination();
            }
            else
            {
                Debug.Log("Insufficient patrol points");
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)                                     //change target if collide with other npc or player
    {
        if (other.CompareTag("NPC")|| other.CompareTag("Player"))
        {
            ChangePatrolPoint();
            SetDestination();
            Debug.Log("NPC detected");
        }
        else if (other.CompareTag("PickedUp"))
        {
            collidedObj.Add(other.gameObject);
            Debug.Log("Box detected");
            _waiting = true;
        }
        else
        {
            _waiting = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickedUp"))
        {

            for (int i = 0; i < collidedObj.Count; i++)
            {
                collidedObj.RemoveAt(collidedObj.Count - 1);
            }
        }
    }
    public void FixedUpdate()
    {
        if (_travaling && _navMeshAgent.remainingDistance <= 1.0f)
        {
            _travaling = false;
            if (_patrolWaiting)
            {
                _waiting = true;
                _waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }
        if (_waiting)
        {
            _waitTimer += Time.fixedDeltaTime;
            if (_waitTimer >= _totalWaitTime)
            {
                _waiting = false;

                ChangePatrolPoint();
                SetDestination();
                if (collidedObj.Count != 0)
                {
                    npcTrash += collidedObj[collidedObj.Count - 1].GetComponent<ItemCost>().boxCost;
                    Destroy(collidedObj[collidedObj.Count - 1]);
                    collidedObj.RemoveAt(collidedObj.Count - 1);
                }
            }
        }
    }

    private void SetDestination()                                                       //directs npc to target position
    {
        if (_patrolPoints != null)
        {
            Vector3 targetVector = _patrolPoints[_currentPatrolIndex].transform.position;
            _navMeshAgent.SetDestination(targetVector);
            _travaling = true;
        }
    }
    private void ChangePatrolPoint()                                                    //change point to patrol
    {
        if (UnityEngine.Random.Range(0f, 1f) <= _switchProbability) 
        {
            _patrolForward = !_patrolForward;
        }
        if (_patrolForward)
        {
            _currentPatrolIndex = (_currentPatrolIndex) % _patrolPoints.Count;
            Debug.Log("Error here");
        }
        else
        {
            if (--_currentPatrolIndex < 0)
            {
                _currentPatrolIndex = _patrolPoints.Count - 1;
            }
        }
    }    
}
