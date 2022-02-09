using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColorPalette
{
    public Color Primary { get; set; }
    public Color Secondary { get; set; }
    public Color Tertiary { get; set; }
    public Color Quaternary { get; set; }
    public Color Navbar { get; set; }
    public Color PageBackground { get; set; }
    public Color PageForeground { get; set; }
    public Color PrimaryText { get; set; }
    public Color SecondaryText { get; set; }
    public Color Title { get; set; }
    public Color Header { get; set; }
    public Color Span { get; set; }
    public Color Caption { get; set; }
    public Color Subtitle { get; set; }
    public Color Hint { get; set; }
}
