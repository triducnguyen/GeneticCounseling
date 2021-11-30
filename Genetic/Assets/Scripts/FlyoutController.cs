using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyoutController : MonoBehaviour
{
    public Animator animator;
    public bool visible = false;

    private void Awake()
    {
        animator.Play("FlyoutClose", -1, 1);
    }

    public void FlyoutTapped()
    {
        visible = !visible;
        if (visible)
        {
            animator.Play("FlyoutOpen");
        }
        else
        {
            animator.Play("FlyoutClose");
        }
    }
}
