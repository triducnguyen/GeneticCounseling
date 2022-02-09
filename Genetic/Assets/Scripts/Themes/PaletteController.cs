using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteController : Singleton<PaletteController>
{
    public event ColorPaletteChangedDelegate ColorsChanged;
    public delegate void ColorPaletteChangedDelegate(ColorPaletteChangedEventArgs _args);
    
    public List<ColorPalette> palettes = new List<ColorPalette>();
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
    ColorPalette _palette;

    private void Awake()
    {
        currentPalette = palettes[0];
    }

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
}
