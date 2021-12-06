using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavbarButtonController : MonoBehaviour
{
    public FlyoutController flyout;
    public Animator animator;
    public bool playing = false;
    private bool alreadyOpen;
    public void Awake()
    {
        //stop animator
        //animator.speed = 0;
        animator.Play("RotateBack", -1, 1);
    }

    public void Tapped()
    {
        var animInfo = animator.GetCurrentAnimatorStateInfo(0);
        float normT = animInfo.normalizedTime % 1f;
        //check if animation is playing
        float newNorm = playing ? 1f - normT : 0;
        if (flyout.visible)
        {
            animator.Play("RotateBack", -1, newNorm);
            alreadyOpen = false;
        }
        else
        {
            animator.Play("Rotate", -1, newNorm);
            alreadyOpen = true;
        }
        flyout.FlyoutTapped();
    }

    public void Swipped()
    {
        var animInfo = animator.GetCurrentAnimatorStateInfo(0);
        float normT = animInfo.normalizedTime % 1f;
        //check if animation is playing
        float newNorm = playing ? 1f - normT : 0;
        if (!flyout.visible && alreadyOpen)
        {
            animator.Play("RotateBack", -1, newNorm);
            alreadyOpen = false;
        }
        else if(flyout.visible && !alreadyOpen)
        {
            animator.Play("Rotate", -1, newNorm);
            alreadyOpen = true;
        }
    }
}
