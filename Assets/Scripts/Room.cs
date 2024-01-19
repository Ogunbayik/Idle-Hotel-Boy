using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private bool isOpen;
    void Start()
    {
        isOpen = false;
    }

    void Update()
    {
        
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
