using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyoutItemController : MonoBehaviour
{
    public string pageName = "";
    public Text title;
    public Image icon;
    public Image background;
    public Button button;
    public Action action;

    PaletteController controller { get =>  AppController.instance.controller;}

    public void Start()
    {
        controller.ColorsChanged += ColorsChanged;
        ColorsChanged(new ColorPaletteChangedEventArgs(controller.currentPalette));
    }


    public void OnTap()
    {
        action.Invoke();
    }
    public void ColorsChanged(ColorPaletteChangedEventArgs args)
    {
        title.color = args.palette.FlyoutText;
        icon.color = args.palette.FlyoutIcon;
        background.color = args.palette.FlyoutItemBackground;
    }
}
