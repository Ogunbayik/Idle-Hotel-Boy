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

    private Room room;
    private Furniture[] furnitures;

    private Vector3 insidePosition;
    private Vector3 waitDoorPosition;
    private Vector3 outsidePosition;
    private Vector3 desiredPosition;

    private float movementSpeed = 3f;
    private float startOpenTimer = 2f;
    private float startOutsideTimer = 5f;

    private float openTimer;
    private float outsideTimer;
    private float desiredDistance;

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

        furnitures = room.GetComponentsInChildren<Furniture>();
    }

    void Update()
    {
        switch(currentPosition)
        {
            case MovePositions.Door:
                //WAITING DOOR
                desiredDistance = GetDesiredDistance();
                if (desiredDistance <= 0.1f)
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
                //GOING INSIDE
                desiredDistance = GetDesiredDistance();
                if (desiredDistance <= 0.1f)
                {
                    room.CloseDoor();
                    room.IsOpen(false);
                    outsideTimer -= Time.deltaTime;

                    if( outsideTimer <= 0)
                    {
                        //MESS UP
                        foreach (var furniture in furnitures)
                        {
                            furniture.MessUp(true);
                        }
                        
                        room.IdleDoorAnimation();
                        outsideTimer = startOutsideTimer;
                        desiredPosition = waitDoorPosition;
                        currentPosition = MovePositions.Outside;
                    }
                }
                break;
            case MovePositions.Outside:
                //GOING WAITING POINT
                room.OpenDoor();
                room.IsOpen(true);
                desiredDistance = GetDesiredDistance();

                if (desiredDistance <= 0.1f)
                {
                    //GOING OUTSIDE POINT
                    desiredPosition = outsidePosition;
                    room.CloseDoor();
                    room.IsOpen(false);

                    var distance = GetDesiredDistance();
                    if (distance <= 0.1f)
                    {
                        room.IdleDoorAnimation();
                        room.IsReady(false);
                    }
                }
                break;
        }

        HandleMovement(desiredPosition);
    }

    private float GetDesiredDistance()
    {
        var distance = Vector3.Distance(transform.position, desiredPosition);
        return distance;
    }

    private void HandleMovement(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, movementSpeed * Time.deltaTime);
    }
}
