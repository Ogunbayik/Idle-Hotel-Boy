using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string HORIZONTAL_INPUT = "Horizontal";
    private const string VERTICAL_INPUT = "Vertical";

    [Header(" Settings ")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask checkLayer;

    private Vector3 movementDirection;

    private float horizontalInput;
    private float verticalInput;
    void Start()
    {
        
    }

    void Update()
    {
        HandleMovement();

        CheckInteractableObject();

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    private void CheckInteractableObject()
    {
        var checkSphere = Physics.CheckSphere(transform.position, checkRadius, checkLayer);
        var interact = Input.GetKey(KeyCode.E);

        if (checkSphere && interact)
        {
            Debug.Log("Interacted");
        }
    }

    private void HandleMovement()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL_INPUT);
        verticalInput = Input.GetAxis(VERTICAL_INPUT);

        movementDirection = new Vector3(horizontalInput, 0f, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
    }
}
