using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizItem : StyleHandler
{

    public Text name;
    public Toggle toggle;
    public SavedQuiz quiz
    {
        get => _q;
        set
        {
            _q = value;
            name.text = _q.name;
        }
    }
    SavedQuiz _q;
    public Image image;

    public override void ColorsChanged(ColorPaletteChangedEventArgs args)
    {
        image.color = args.palette.ItemBase;
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
        name.color = args.palette.ItemText;
    }
}
