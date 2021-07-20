using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DirectedAgent : MonoBehaviour
{

   [SerializeField] public NavMeshAgent agent;
    public Vector3 targetPosition;
    public bool completePath;

    // Use this for initialization
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    public void SetTarget(Vector3 pos) {
        targetPosition = pos;

    }
    private void Update()
    {
        MoveToLocation(targetPosition);
       
    }
    public void MoveToLocation(Vector3 targetPoint)
    {
        agent.destination = targetPoint;
        transform.GetComponentInChildren<Animator>().SetFloat("Speed", agent.velocity.magnitude);
        agent.isStopped = false;
    }
}