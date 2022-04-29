using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>A Quiz Item toggle.</summary>
public class QuizItem : StyleHandler
{

    /// <summary>The quiz name.</summary>
    public Text name;
    /// <summary>The quiz toggle component.</summary>
    public Toggle toggle;
    /// <summary>Gets or sets the quiz.</summary>
    /// <value>The quiz.</value>
    public SavedQuiz quiz
    {
        get => _q;
        set
        {
            _q = value;
            name.text = _q.name;
        }
    }
    /// <summary>The private value of the quiz.</summary>
    SavedQuiz _q;
    /// <summary>The item background.</summary>
    public Image image;

    /// <summary>Event handler for when colors change.</summary>
    /// <param name="args">The <see cref="ColorPaletteChangedEventArgs" /> instance containing the color palette data.</param>
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
