using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterButtonController : MonoBehaviour
{
    public Animator animator;
    public bool visible = false;
    public bool playing = false;

    public void OnTap()
    {
        var animInfo = animator.GetCurrentAnimatorStateInfo(0);
        float normT = animInfo.normalizedTime % 1f;
        float newNorm = playing ? 1f - normT : 0;
        if (!visible)
        {
            animator.Play("FilterOn", -1, newNorm);
        }
        else
        {
            animator.Play("FilterOff", -1, newNorm);
        }
        visible = !visible;
    }

    private void OnEnable()
    {
        if (!visible)
        {
            animator.Play("FilterOff", -1, 1);
        }
        else
        {
            animator.Play("FilterOn", -1, 1);
        }
    }
}
