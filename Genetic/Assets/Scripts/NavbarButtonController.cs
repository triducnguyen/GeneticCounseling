using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavbarButtonController : MonoBehaviour
{
    public FlyoutController flyout;
    public Animator animator;

    public void Awake()
    {
        //stop animator
        //animator.speed = 0;
        animator.Play("RotateBack", -1, 1);
    }

    public void Tapped()
    {
        if (flyout.visible)
        {
            animator.Play("RotateBack");
        }
        else
        {
            animator.Play("Rotate");
        }
        flyout.FlyoutTapped();
        Debug.Log("Tapped");
    }
}
