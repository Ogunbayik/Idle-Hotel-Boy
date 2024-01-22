using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string HORIZONTAL_INPUT = "Horizontal";
    private const string VERTICAL_INPUT = "Vertical";

    private Room room;
    private Furniture closestFurniture;

    [Header(" Settings ")]
    [SerializeField] private float movementSpeed;

    private Vector3 movementDirection;

    private float horizontalInput;
    private float verticalInput;
    private float leastDistance;

    private bool canMove;

    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private LayerMask furnitureLayer;

    void Start()
    {
        room = null;
    }

    void Update()
    {
        HandleMovement();

        CheckEnterPlace();
        CheckClosestFurniture();

        var interact = Input.GetKey(KeyCode.E);
        if(interact && closestFurniture != null)
        {
            closestFurniture.TidyUp(true);
        }
    }

    #region CheckPlace
    private void CheckEnterPlace()
    {
        var checkInteractable = Physics.CheckSphere(transform.position, 1f, interactLayer);

        if (checkInteractable)
        {
            room = FindObjectOfType<Room>();

            if (Input.GetKey(KeyCode.E))
                room.OpenDoor();
        }
        else
        {
            room = null;
        }
    }
    #endregion

    #region CheckFurniture
    private void CheckClosestFurniture()
    {
        var checkFurniture = Physics.CheckSphere(transform.position, 1f, furnitureLayer);
        if (checkFurniture)
        {
            var allFurniture = FindObjectsOfType<Furniture>();

            leastDistance = Mathf.Infinity;
            foreach (var furniture in allFurniture)
            {
                var furnitureDistance = Vector3.Distance(transform.position, furniture.transform.position);
                if (furnitureDistance < leastDistance)
                {
                    leastDistance = furnitureDistance;
                    closestFurniture = furniture;
                }
            }
        }
        else
        {
            leastDistance = Mathf.Infinity;
            closestFurniture = null;
        }
    }
    #endregion

    #region Movement

    private void HandleMovement()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL_INPUT);
        verticalInput = Input.GetAxis(VERTICAL_INPUT);

        movementDirection = new Vector3(horizontalInput, 0f, verticalInput);
        movementDirection.Normalize();

        var playerHeight = 2f;
        var playerRadius = 0.6f;
        var movementDistance = movementSpeed * Time.deltaTime;
        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movementDirection, movementDistance);

        if(!canMove)
        {
            Vector3 movementDirectionX = new Vector3(horizontalInput, 0f, 0f);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movementDirectionX, movementDistance);

            if(canMove)
            {
                movementDirection = movementDirectionX;
            }
            else
            {
                Vector3 movementDirectionZ = new Vector3(0f, 0f, verticalInput);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movementDirectionZ, movementDistance);

                if (canMove)
                    movementDirection = movementDirectionZ;
            }
        }

        if (canMove)
            transform.position += movementDirection * movementDistance;
    }
    #endregion
}
