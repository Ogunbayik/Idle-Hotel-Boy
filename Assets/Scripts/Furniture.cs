using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    private const string MESSUP_ANIMATOR_HASH = "MessUp";
    private const string TIDYUP_ANIMATOR_HASH = "TidyUp";

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void MessUp(bool isMessUp)
    {
        animator.SetBool(MESSUP_ANIMATOR_HASH, isMessUp);
    }

    public void TidyUp(bool isTidyUp)
    {
        animator.SetBool(TIDYUP_ANIMATOR_HASH, isTidyUp);
    }
}
