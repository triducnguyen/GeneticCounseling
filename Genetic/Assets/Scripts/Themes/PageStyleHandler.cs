using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageStyleHandler : StyleHandler
{
    public Image backgroundImage;

    public override void ColorsChanged(ColorPaletteChangedEventArgs args)
    {
        backgroundImage.color = args.palette.PageBackground;
        base.ColorsChanged(args);
    }
}
