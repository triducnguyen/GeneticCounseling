using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPalette : MonoBehaviour
{
    public PaletteController controller { get => PaletteController.instance; }

    public Color Primary;
    public Color Secondary;
    public Color Tertiary;
    public Color Quaternary;
    
    public Color Navbar;
    
    public Color FlyoutBtn;
    public Color FlyoutTouchBackground;
    public Color FlyoutHeaderBackground;
    public Color FlyoutBackground;
    public Color FlyoutItemBackground;
    public Color FlyoutText;
    public Color FlyoutIcon;
    public Color FlyoutFooterBackground;
    
    public Color PageBackground;
    public Color PageForeground;

    public Color SelectedItem;

    public Color PrimaryText;
    public Color SecondaryText;
    public Color Title;
    public Color Header;
    public Color Span;
    public Color Caption;
    public Color Subtitle;
    public Color Hint;

    public Color QuestionBackground;
    public Color QuestionText;
    public Color AnswerBackground;
    public Color AnswerText;

    //right colors
    public Color QuestionBackgroundCorrect;
    public Color QuestionTextCorrect;
    //wrong colors
    public Color QuestionBackgroundIncorrect;
    public Color QuestionTextIncorrect;

    public Color FlashcardBackground;
    public Color FlashcardText;

    public static Color Highlight(Color color)
    {
        //add small amount to each channel
        return Highlight(color, .01f);
    }

    public static Color Highlight(Color color, float amount)
    {
        color.r += amount;
        color.g += amount;
        color.b += amount;
        return color;
    }

    public static Color Shadow(Color color)
    {
        return Shadow(color, .01f);
    }

    public static Color Shadow(Color color, float amount)
    {
        return Highlight(color, -amount);
    }

}
public class ColorPaletteChangedEventArgs : EventArgs
{
    public ColorPalette palette { get => _palette; }
    private ColorPalette _palette;
    public ColorPaletteChangedEventArgs(ColorPalette p)
    {
        _palette = p;
    }
}
