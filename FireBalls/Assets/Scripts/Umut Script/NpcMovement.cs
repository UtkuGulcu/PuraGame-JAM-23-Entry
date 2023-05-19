using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcMovement : MonoBehaviour
{
    private NavMeshAgent agent;


    private void Awake()
    {
      agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        agent.SetDestination(NpcManager.Instance.GetShortestWayPoint(transform.position));
        
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, agent.destination)<0.7f)
        {
            
            agent.SetDestination(NpcManager.Instance.GetRandomWayPoint(transform.position));
        }
        
    }

}
