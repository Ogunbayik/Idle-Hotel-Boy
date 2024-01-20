using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    private enum MovePositions
    {
        Door,
        Inside,
        Outside
    }

    private MovePositions currentPosition;

    [SerializeField] private Room room;

    private Vector3 insidePosition;
    private Vector3 waitDoorPosition;
    private Vector3 outsidePosition;
    private Vector3 desiredPosition;

    private float movementSpeed = 5f;
    private float startOpenTimer = 3f;
    private float openTimer;
    private float startOutsideTimer = 5f;
    private float outsideTimer;

    private void Awake()
    {
        room = GameObject.FindObjectOfType<Room>();
    }
    void Start()
    {
        insidePosition = room.gameObject.transform.localPosition;
        waitDoorPosition = room.gameObject.transform.localPosition + new Vector3(0f, 0f, -3.5f);
        outsidePosition = new Vector3(12.5f, 0f, -2f);
        openTimer = startOpenTimer;
        outsideTimer = startOutsideTimer;
        desiredPosition = waitDoorPosition;
    }

    void Update()
    {
        switch(currentPosition)
        {
            case MovePositions.Door:
                var distanceBetweenDoorPosition = Vector3.Distance(transform.position, desiredPosition);
                if (distanceBetweenDoorPosition <= 0.1f)
                {
                    openTimer -= Time.deltaTime;
                    room.OpenDoor();

                    if (openTimer <= 0f)
                    {
                        openTimer = startOpenTimer;
                        room.IsOpen(true);
                        desiredPosition = insidePosition;
                        currentPosition = MovePositions.Inside;
                    }
                }
                break;
            case MovePositions.Inside:
                var distanceBetweenInsidePosition = Vector3.Distance(transform.position, desiredPosition);
                if (distanceBetweenInsidePosition <= 0.1f)
                {
                    room.CloseDoor();
                    outsideTimer -= Time.deltaTime;

                    if( outsideTimer <= 0)
                    {
                        outsideTimer = startOutsideTimer;
                        desiredPosition = waitDoorPosition;
                        currentPosition = MovePositions.Outside;
                    }
                }
                break;
            case MovePositions.Outside:
                room.OpenDoor();
                break;
        }


        HandleMovement(desiredPosition);
    }

    private void HandleMovement(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, movementSpeed * Time.deltaTime);
    }
}
