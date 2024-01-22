using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private const string OPEN_ANIMATOR_HASH = "isOpen";
    private const string CLOSE_ANIMATOR_HASH = "isClose";

    private Animator animator;

    private Furniture[] furnitures;

    private bool isOpen;
    private bool isReady;

    void Start()
    {
        animator = GetComponent<Animator>();
        isOpen = false;
        isReady = true;

        furnitures = GetComponentsInChildren<Furniture>();
    }

    private void Update()
    {
        for (int i = 0; i < furnitures.Length; i++)
        {
            if (furnitures[i].GetIsTidy() == true)
                isReady = true;
        }

    }

    public void OpenDoor()
    {
        animator.SetBool(OPEN_ANIMATOR_HASH, true);
    }
    public void CloseDoor()
    {
        animator.SetBool(CLOSE_ANIMATOR_HASH, true);
    }
    public void IdleDoorAnimation()
    {
        animator.SetBool(OPEN_ANIMATOR_HASH, false);
        animator.SetBool(CLOSE_ANIMATOR_HASH, false);
    }

    public void IsOpen(bool isOpen)
    {
        this.isOpen = isOpen;
    }

    public void IsReady(bool isReady)
    {
        this.isReady = isReady;
    }

    public bool GetIsReady()
    {
        return isReady;
    }
}
