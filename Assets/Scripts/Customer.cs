using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    private enum States
    {
        Wait,
        Move
    }

    private States currentState;

    private NavMeshAgent agent;

    private Transform desiredPoint;
    [SerializeField] private Transform[] movementPoints;
    

    private int moveIndex = 0;

    private float distanceToPoint;
    private float startWaitTime = 3f;
    private float waitTimer;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {

    }


    void Update()
    {
        switch (currentState)
        {
            case States.Wait:
                Waiting();
                break;
            case States.Move:
                Moving(movementPoints[moveIndex]);
                break;
        }

        if (distanceToPoint > 0.2f)
        {
            currentState = States.Move;
        }
        else
        {
            currentState = States.Wait;
        }
    }

    private void Waiting()
    {
        waitTimer -= Time.deltaTime;
        
        if(waitTimer <= 0)
        {
            GoNextDirection();
            waitTimer = startWaitTime;
        }
    }

    private void Moving(Transform transform)
    {
        distanceToPoint = GetDistanceMovePoint(movementPoints[moveIndex]);
        agent.SetDestination(transform.position);
    }
    private void GoNextDirection()
    {
        moveIndex++;
        desiredPoint = movementPoints[moveIndex];
        currentState = States.Move;
        GetDistanceMovePoint(desiredPoint);
    }

    private float GetDistanceMovePoint(Transform point)
    {
        distanceToPoint = 100f;
        distanceToPoint = Vector3.Distance(transform.position, point.transform.position);
        return distanceToPoint;
    }

}
