using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Handles color palette changes. Add elements to list to have them automatically update when a color palette change happens.</summary>
public class StyleHandler : MonoBehaviour
{
    /// <summary>Gets the app controller.</summary>
    /// <value>The app controller.</value>
    public PaletteController controller { get => AppController.instance.controller; }

    /// <summary>The image components to be recolored to the primary color of the app.</summary>
    public List<Image> PrimaryColors;
    /// <summary>The image components to be recolored to the secondary color of the app.</summary>
    public List<Image> SecondaryColors;
    /// <summary>The text components to be recolored.</summary>
    public List<Text> PrimaryTexts;
    /// <summary>The text components to be recolored.</summary>
    public List<Text> SecondaryTexts;
    /// <summary>The text components to be recolored.</summary>
    public List<Text> Titles;
    /// <summary>The text components to be recolored.</summary>
    public List<Text> Headers;
    /// <summary>The text components to be recolored.</summary>
    public List<Text> Spans;
    /// <summary>The text components to be recolored.</summary>
    public List<Text> Captions;
    /// <summary>The text components to be recolored.</summary>
    public List<Text> Subtitles;
    /// <summary>The text components to be recolored.</summary>
    public List<Text> Hints;
    /// <summary>The button components to be recolored.</summary>
    public List<Button> Buttons;
    /// <summary>The scrollbar components to be recolored.</summary>
    public List<Scrollbar> ScrollBars;
    /// <summary>The image components to be recolored.</summary>
    public List<Image> ScrollViewBackground;

    

    protected virtual void Awake()
    {
        //subscribe to colorschanged event
        controller.ColorsChanged += ColorsChanged;
        //set current colors
        UpdateColors();
    }

    /// <summary>Manually triggers a pallete color update.</summary>
    public void UpdateColors()
    {
        ColorsChanged(new ColorPaletteChangedEventArgs(controller.currentPalette));
    }

    /// <summary>Event handler for when colors change.</summary>
    /// <param name="args">The <see cref="ColorPaletteChangedEventArgs" /> instance containing the color palette data.</param>
    public virtual void ColorsChanged(ColorPaletteChangedEventArgs args)
    {
        //set all colors
        foreach (var pt in PrimaryTexts)
        {
            pt.color = args.palette.PrimaryText;
        }
        foreach (var pt in SecondaryTexts)
        {
            pt.color = args.palette.SecondaryText;
        }
        foreach (var t in Titles)
        {
            t.color = args.palette.Title;
        }
        foreach (var h in Headers)
        {
            h.color = args.palette.Header;
        }
        foreach (var s in Spans)
        {
            s.color = args.palette.Span;
        }
        foreach (var c in Captions)
        {
            c.color = args.palette.Caption;
        }
        foreach (var su in Subtitles)
        {
            su.color = args.palette.Subtitle;
        }
        foreach (var h in Hints)
        {
            h.color = args.palette.Hint;
        }
        foreach (var p in PrimaryColors)
        {
            p.color = args.palette.Primary;
        }
        foreach (var s in SecondaryColors)
        {
            s.color = args.palette.Secondary;
        }

        foreach(var b in Buttons)
        {
            b.GetComponent<Image>().color = args.palette.ButtonBase;
            b.colors = new ColorBlock()
            {
                normalColor = args.palette.ButtonNormal,
                disabledColor = args.palette.ButtonDisabled,
                highlightedColor = args.palette.ButtonHover,
                pressedColor = args.palette.ButtonPressed,
                selectedColor = args.palette.ButtonSelected,
                fadeDuration = b.colors.fadeDuration,
                colorMultiplier = b.colors.colorMultiplier
            };
            b.GetComponentInChildren<Text>().color = args.palette.ButtonText;
        }

        foreach(var s in ScrollBars)
        {
            s.GetComponent<Image>().color = args.palette.scrollBarBackground;
            s.targetGraphic.color = args.palette.scrollBarBase;
            s.colors = new ColorBlock()
            {
                normalColor = args.palette.ButtonNormal,
                disabledColor = args.palette.ButtonDisabled,
                highlightedColor = args.palette.ButtonHover,
                pressedColor = args.palette.ButtonPressed,
                selectedColor = args.palette.ButtonSelected,
                fadeDuration = s.colors.fadeDuration,
                colorMultiplier = s.colors.colorMultiplier
            };
        }

        foreach(var sv in ScrollViewBackground)
        {
            sv.color = args.palette.scrollViewBackground;
        }
    }
}
