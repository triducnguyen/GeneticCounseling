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
        
    }

    public override void ColorsChanged(ColorPaletteChangedEventArgs args)
    {
        background.color = args.palette.ItemBase;
        checkBackground.color = args.palette.CheckboxBackground;
        toggle.colors = new ColorBlock()
        {
            normalColor = args.palette.ItemNormal,
            disabledColor = ColorPalette.Shadow(args.palette.ItemNormal),
            highlightedColor = ColorPalette.Highlight(args.palette.ItemNormal),
            pressedColor = ColorPalette.Shadow(args.palette.ItemNormal),
            selectedColor = args.palette.ItemSelected,
            colorMultiplier = toggle.colors.colorMultiplier,
            fadeDuration = toggle.colors.fadeDuration
        };
        tagText.color = args.palette.ItemText;
        check.color = args.palette.CheckColor;
    }

}
