using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manages the flyout menu.</summary>
public class FlyoutController : StyleHandler
{
    /// <summary>The animator
    /// component that animates the flyout menu.</summary>
    public Animator animator;
    /// <summary>The list of flyout page items.</summary>
    public GameObject itemList;
    /// <summary>The touch background
    /// of they flyout menu.</summary>
    public Image touchBackground;
    /// <summary>The alpha
    /// of the touch background.</summary>
    public float alpha;
    /// <summary>If the flyout is visible or not.</summary>
    public bool visible = false;
    /// <summary>If the animator component is playing.</summary>
    public bool playing = false;

    protected override void Awake()
    {
        animator.Play("FlyoutClose", -1, 1);
        base.Awake();
    }

    private void Update()
    {
        if (playing)
        {
            var current = touchBackground.color;
            touchBackground.color = new Color(current.r, current.g, current.b, alpha);
        }
    }

    /// <summary>Called when the flyout button is tapped.</summary>
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

    /// <summary>Selects the currently open page.</summary>
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

    /// <summary>An event handler for when the colors change.</summary>
    /// <param name="args">The <see cref="ColorPaletteChangedEventArgs" /> instance containing the color palette data.</param>
    public override void ColorsChanged(ColorPaletteChangedEventArgs args)
    {
        touchBackground.color = args.palette.FlyoutTouchBackground;
        base.ColorsChanged(args);
    }
}
