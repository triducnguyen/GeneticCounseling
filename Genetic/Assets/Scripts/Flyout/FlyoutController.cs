using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlyoutController : MonoBehaviour
{
    public Animator animator;
    public GameObject itemList;
    public bool visible = false;
    public bool playing = false;

    private void Awake()
    {
        animator.Play("FlyoutClose", -1, 1);
    }

    public void FlyoutTapped()
    {
        
        var animInfo = animator.GetCurrentAnimatorStateInfo(0);
        float normT = animInfo.normalizedTime % 1f;
        //check if animation is playing
        float newNorm = playing ? 1f - normT : 0;
        if (!visible)
        {
            animator.Play("FlyoutOpen", -1, newNorm);
            SelectCurrent();
            
        }
        else
        {
            animator.Play("FlyoutClose", -1, newNorm);
        }
        visible = !visible;
    }

    void SelectCurrent()
    {
        //select currently open page
        var current = NavigationController.instance.currentPage;
        List<FlyoutItemController> items = itemList.GetComponentsInChildren<FlyoutItemController>().ToList();
        FlyoutItemController item = items.Find(i => i.pageName == current.pageName);
        if (item != null)
        {
            item.button.Select();
        }
    }
}
