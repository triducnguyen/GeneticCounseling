using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>A navigation item to be displayed in the flyout item list.</summary>
public class FlyoutItemController : StyleHandler
{
    /// <summary>The page name.</summary>
    public string pageName = "";
    /// <summary>The item title.</summary>
    public Text title;
    /// <summary>The item icon.</summary>
    public Image icon;
    /// <summary>The item background.</summary>
    public Image background;
    /// <summary>The item button component.</summary>
    public Button button;
    /// <summary>The item action
    /// to perform when tapped.</summary>
    public Action action;

    /// <summary>Called when item is tapped.</summary>
    public void OnTap()
    {
        action.Invoke();
    }
    public override void ColorsChanged(ColorPaletteChangedEventArgs args)
    {
        title.color = args.palette.FlyoutText;
        icon.color = args.palette.FlyoutIcon;
        background.color = args.palette.FlyoutItemBackground;
        base.ColorsChanged(args);
    }
}
