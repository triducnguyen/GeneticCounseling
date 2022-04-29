using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Represents a tag/category.</summary>
public class TagItem : StyleHandler
{
    /// <summary>The item background image.</summary>
    public Image background;
    /// <summary>The toggle check
    /// image.</summary>
    public Image check;
    /// <summary>The check background
    /// image.</summary>
    public Image checkBackground;

    /// <summary>The tag text.</summary>
    public Text tagText;
    /// <summary>The toggle component.</summary>
    public Toggle toggle;

    /// <summary>Event handler for when colors change.</summary>
    /// <param name="args">The <see cref="ColorPaletteChangedEventArgs" /> instance containing the color palette data.</param>
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
