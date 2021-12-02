using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyoutController : MonoBehaviour
{
    public Animator animator;
    public bool visible = false;
    public bool playing = false;

    private void Awake()
    {
        animator.Play("FlyoutClose", -1, 1);
    }

    public void FlyoutTapped()
    {
        visible = !visible;
        var animInfo = animator.GetCurrentAnimatorStateInfo(0);
        float normT = animInfo.normalizedTime % 1f;
        //check if animation is playing
        float newNorm = playing ? 1f - normT : 0;
        if (visible)
        {
            animator.Play("FlyoutOpen", -1, newNorm);
        }
        else
        {
            animator.Play("FlyoutClose", -1, newNorm);
        }
    }
}
