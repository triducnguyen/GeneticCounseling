using App.Themes;
using App.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Themes
{
/// <summary>Controls the current theme / color pallette.</summary>
public class PaletteController : Singleton<PaletteController>
{
    /// <summary>Occurs when palette/theme has changed.</summary>
    public event ColorPaletteChangedDelegate ColorsChanged;
    /// <summary>Custom delegate to send custom event arguments.</summary>
    /// <param name="_args">The <see cref="ColorPaletteChangedEventArgs" /> instance containing the color palette data.</param>
    public delegate void ColorPaletteChangedDelegate(ColorPaletteChangedEventArgs _args);

    /// <summary>The color palettes available.</summary>
    public List<ColorPalette> palettes = new List<ColorPalette>();
    /// <summary>Gets or sets the current palette.</summary>
    /// <value>The current palette.</value>
    public ColorPalette currentPalette
    {
        get => _palette;
        set
        {
            _palette = value;
            //let everything know that the palette was changed
            if (ColorsChanged != null)
            {
                ColorsChanged.Invoke(new ColorPaletteChangedEventArgs(_palette));
            }
        }
    }
    /// <summary>The palette's private value.</summary>
    ColorPalette _palette;

    /// <summary>Called when the object is instantiated.</summary>
    protected override void Awake()
    {
        currentPalette = palettes[0];
        base.Awake();
    }

    /// <summary>Advances to the next color palette.</summary>
    public void NextPalette()
    {
        int idx = palettes.IndexOf(currentPalette);
        idx++;
        if (idx >= palettes.Count)
        {
            idx = 0;
        }
        currentPalette = palettes[idx];
    }
    /// <summary>Returns to the previous palette.</summary>
    public void PreviousPalette()
    {
        int idx = palettes.IndexOf(currentPalette);
        idx--;
        if (idx == 0)
        {
            idx = palettes.Count - 1;
        }
        currentPalette = palettes[idx];
    }

    /// <summary>Goes to the specified palette.</summary>
    /// <param name="palette">The palette.</param>
    public void GotoPalette(int palette)
    {
        currentPalette = palettes[palette];
    }
}

}

