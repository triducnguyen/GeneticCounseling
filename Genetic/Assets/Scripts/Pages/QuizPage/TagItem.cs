using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagItem : StyleHandler
{
    public Image background;
    public Image check;
    public Image checkBackground;

    public Text tagText;
    public Toggle toggle;


    public void OnChange()
    {
        if (toggle.isOn)
        {
            //set selected
            background.color = controller.currentPalette.ItemSelected;
        }
        else
        {
            //unset selected
            background.color = controller.currentPalette.ItemNormal;
        }
    }

    public override void ColorsChanged(ColorPaletteChangedEventArgs args)
    {
        toggle.colors = new ColorBlock()
        {
            normalColor = args.palette.ItemNormal,
            disabledColor = ColorPalette.Shadow(args.palette.ItemNormal),
            highlightedColor = ColorPalette.Highlight(args.palette.ItemNormal),
            pressedColor = ColorPalette.Shadow(args.palette.ItemNormal),
            selectedColor = args.palette.ItemSelected,
        };
        if (toggle.isOn)
        {
            background.color = args.palette.ItemSelected;
        }
        else
        {
            background.color = args.palette.ItemNormal;
        }
        tagText.color = args.palette.ItemText;
        check.color = args.palette.CheckColor;
        checkBackground.color = args.palette.CheckboxBackground;
    }

}
