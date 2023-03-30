using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] float decisonDelay = 3f;
    [SerializeField] Transform objectToChase;
    [SerializeField] Transform[] waypoints = new Transform[5];
    public int currentWaypoint = 0;
    [SerializeField] EnemyStates currentState;

    enum EnemyStates
    {
        Patrolling,
        Chasing
    }
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("GetDestination", 1.5f, decisonDelay);
        currentState = EnemyStates.Patrolling;
        agent.SetDestination(waypoints[currentWaypoint].position);
    }

    public void GetDestination()
    {
        if (currentState == EnemyStates.Chasing)
        {
            agent.SetDestination(objectToChase.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, objectToChase.position) > 10f)
        {
            currentState = EnemyStates.Patrolling;
        }
        else
        {
            currentState = EnemyStates.Chasing;
        }
        if (currentState == EnemyStates.Patrolling)
        {
            if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) <= 2f)
            {
                currentWaypoint++;
                if (currentWaypoint == waypoints.Length)
                {
                    currentWaypoint = 0;
                    
                }
                Debug.Log("Moving to " + waypoints[currentWaypoint].position);
                agent.SetDestination(waypoints[currentWaypoint].position);

            }
            
        }
    }
}
