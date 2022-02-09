using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FlyoutController : MonoBehaviour
{
    public Animator animator;
    public GameObject itemList;
    public Image touchBackground;
    public PaletteController controller { get => AppController.instance.controller; }
    public float alpha;
    public bool visible = false;
    public bool playing = false;

    private void Awake()
    {
        animator.Play("FlyoutClose", -1, 1);
        controller.ColorsChanged += ColorsChanged;
        ColorsChanged(new ColorPaletteChangedEventArgs(controller.currentPalette));
    }

    private void Update()
    {
        if (playing)
        {
            var current = touchBackground.color;
            touchBackground.color = new Color(current.r, current.g, current.b, alpha);
        }
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
            SelectCurrent();        }
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

    public void ColorsChanged(ColorPaletteChangedEventArgs args)
    {
        touchBackground.color = args.palette.FlyoutTouchBackground;
    }
}
