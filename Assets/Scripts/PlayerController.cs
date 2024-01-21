using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string HORIZONTAL_INPUT = "Horizontal";
    private const string VERTICAL_INPUT = "Vertical";

    private Room room;

    [Header(" Settings ")]
    [SerializeField] private float movementSpeed;

    private Vector3 movementDirection;

    private float horizontalInput;
    private float verticalInput;

    private bool canMove;

    void Start()
    {
        room = null;
    }

    void Update()
    {
        HandleMovement();

        RaycastHit hit;

        if(Physics.Raycast(transform.position, movementDirection, out hit, 2f))
        {
            Debug.Log(hit.transform.gameObject.name);
            room = hit.transform.gameObject.GetComponentInParent<Room>();
        }
        else
        {
            room = null;
        }
        

    }

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
