using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Creature))]
public class CreatureMove : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private Vector3 currentPosition;
    [SerializeField]
    private float speed;
    [SerializeField]
    private List<Vector3> destinationTargets = new List<Vector3>();
    [SerializeField]
    private Vector3 currentDestinationTarget;
    [SerializeField]
    private bool idle;
    [SerializeField]
    private Vector3 lastDestinationTarget;
    private float stoppingDistance;
    [SerializeField]
    private float currentDistance;

    private void Awake()
    {
        if (navMeshAgent == null)
            throw new PersonException("NavMeshAgent == null");
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
        navMeshAgent.stoppingDistance = stoppingDistance;
    }

    public void ChangeStopingDistance(float newValue)
    {
        stoppingDistance = newValue;
        navMeshAgent.stoppingDistance = stoppingDistance;
    }
    public float GetStopingDistance()
    {
        return navMeshAgent.stoppingDistance;
    }
    private void FixedUpdate()
    {
        currentPosition = transform.position;
        if (currentDestinationTarget == null || currentDestinationTarget == Vector3.zero)
            SetNextDestinationTarget();
        idle = currentDestinationTarget == null || currentDestinationTarget==Vector3.zero;

        if (currentDestinationTarget != null&& currentDestinationTarget != Vector3.zero)
        {
            currentDistance = Vector3.Distance(this.transform.position, currentDestinationTarget);
            if (!navMeshAgent.pathPending && navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete 
                && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                FinishMovement();
            }
        }
    }

    private void SetNextDestinationTarget()
    {
        if (destinationTargets!=null && destinationTargets.Count>0 && destinationTargets[0] != null)
        {
            currentDestinationTarget = destinationTargets[0];
            if (CanMove())
                navMeshAgent.SetDestination(currentDestinationTarget);
            else
                throw new PersonException("Creature is stoped");
        }
    
    }
    private void FinishMovement()
    {
        lastDestinationTarget = destinationTargets[0];
        destinationTargets.RemoveAt(0);
        currentDestinationTarget = Vector3.zero;
        //currentDestinationTarget = Vector3.zero;
        //navMeshAgent.SetDestination(currentDestinationTarget);
    }
    public void AddDestinationTarget(Vector3 newDestinationTarget)
    {
        Debug.Log(newDestinationTarget + " added to destination targets");
        destinationTargets.Add(newDestinationTarget);
    }

    public void SetStopMovement(bool isStop)
    {
        navMeshAgent.isStopped = isStop;
    }

    public bool CanMove()
    {
        return navMeshAgent.isStopped == false;
    }

    public bool isArrived()
    {
        return currentDestinationTarget == Vector3.zero && destinationTargets.Count==0;
    }
    public Vector3 GetLastDestinationTarget()
    {
        return lastDestinationTarget;
    }
    public float GetDistanse()
    {
        return currentDistance;
    }
}
