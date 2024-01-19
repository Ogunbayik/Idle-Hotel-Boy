using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void MessUp(bool isMessUp)
    {
        animator.SetBool("MessUp", isMessUp);
    }

    public void TidyUp(bool isTidyUp)
    {
        animator.SetBool("TidyUp", isTidyUp);
    }
}
