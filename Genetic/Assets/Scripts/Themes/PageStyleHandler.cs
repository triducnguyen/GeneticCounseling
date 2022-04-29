using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>A StyleHandler for app pages.</summary>
public class PageStyleHandler : StyleHandler
{
    /// <summary>The background of the page.</summary>
    public Image backgroundImage;

    /// <summary>Event handler for when colors change.</summary>
    /// <param name="args">The <see cref="ColorPaletteChangedEventArgs" /> instance containing the color palette data.</param>
    public override void ColorsChanged(ColorPaletteChangedEventArgs args)
    {
        backgroundImage.color = args.palette.PageBackground;
    }
}
