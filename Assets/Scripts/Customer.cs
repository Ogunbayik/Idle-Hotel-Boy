using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    [SerializeField] private Room room;
    private GameObject door;

    private NavMeshAgent agent;
    private Vector3 insidePosition;
    private Vector3 waitDoorPosition;

    private float startWaitTimer = 3f;
    private float waitTimer;

    private void Awake()
    {
        room = GameObject.FindObjectOfType<Room>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        insidePosition = room.gameObject.transform.localPosition;
        waitDoorPosition = room.gameObject.transform.localPosition + new Vector3(0f, 0f, -3.5f);
        waitTimer = startWaitTimer;
    }

    void Update()
    {
        var distanceBetweenDoorPosition = Vector3.Distance(transform.position, waitDoorPosition);
        if (distanceBetweenDoorPosition <= 0.1f)
        {
            waitTimer -= Time.deltaTime;

            //Deactive room door

            if (waitTimer <= 0f)
            {
                waitTimer = startWaitTimer;
                room.IsOpen(true);
            }
        }

        Movement();
    }

    private void Movement()
    {
        var isOpen = room.GetIsOpen();
        if (!isOpen)
        {
            HandleMovement(waitDoorPosition);
        }
        else
        {
            HandleMovement(insidePosition);
        }
    }

    private void HandleMovement(Vector3 position)
    {
        agent.SetDestination(position);
    }
}
