using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool isOpen;
    void Start()
    {
        animator = GetComponent<Animator>();
        isOpen = false;
    }

    void Update()
    {
        
    }

    public void OpenDoor()
    {
        animator.SetBool("isOpen", true);
    }
    public void CloseDoor()
    {
        animator.SetBool("isClose", true);
    }
    public void IdleDoorAnimation()
    {
        animator.SetBool("isOpen", false);
        animator.SetBool("isClose", false);
    }

    public void IsOpen(bool isOpen)
    {
        this.isOpen = isOpen;
    }

    public bool GetIsOpen()
    {
        return isOpen;
    }
}
