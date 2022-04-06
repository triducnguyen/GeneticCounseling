using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StyleHandler : MonoBehaviour
{
    public PaletteController controller { get => AppController.instance.controller; }

    public List<Image> PrimaryColors;
    public List<Image> SecondaryColors;
    public List<Text> PrimaryTexts;
    public List<Text> SecondaryTexts;
    public List<Text> Titles;
    public List<Text> Headers;
    public List<Text> Spans;
    public List<Text> Captions;
    public List<Text> Subtitles;
    public List<Text> Hints;

    public List<Button> Buttons;
    public List<Scrollbar> ScrollBars;
    public List<Image> ScrollViewBackground;

    

    protected virtual void Awake()
    {
        //subscribe to colorschanged event
        controller.ColorsChanged += ColorsChanged;
        //set current colors
        UpdateColors();
    }

    public void UpdateColors()
    {
        ColorsChanged(new ColorPaletteChangedEventArgs(controller.currentPalette));
    }

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
            b.colors = new ColorBlock()
            {
                normalColor = args.palette.ButtonNormal,
                disabledColor = args.palette.ButtonDisabled,
                highlightedColor = args.palette.ButtonHover,
                pressedColor = args.palette.ButtonPressed,
                fadeDuration = b.colors.fadeDuration,
                colorMultiplier = b.colors.colorMultiplier
            };
            b.GetComponentInChildren<Text>().color = args.palette.ButtonText;
        }

        foreach(var s in ScrollBars)
        {
            s.GetComponent<Image>().color = args.palette.scrollBarBackground;
            s.colors = new ColorBlock()
            {
                normalColor = args.palette.ButtonNormal,
                disabledColor = args.palette.ButtonDisabled,
                highlightedColor = args.palette.ButtonHover,
                pressedColor = args.palette.ButtonPressed,
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
