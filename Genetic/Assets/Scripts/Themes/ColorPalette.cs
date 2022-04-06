using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPalette : MonoBehaviour
{
    public PaletteController controller { get => PaletteController.instance; }

    //main colors
    public Color Primary;
    public Color Secondary;
    public Color Tertiary;
    public Color Quaternary;
    
    //navigation bar
    public Color Navbar;
    
    //flyout menu
    public Color FlyoutBtn;
    public Color FlyoutTouchBackground;
    public Color FlyoutHeaderBackground;
    public Color FlyoutBackground;
    public Color FlyoutItemBackground;
    public Color FlyoutText;
    public Color FlyoutIcon;
    public Color FlyoutFooterBackground;
    
    //pages
    public Color PageBackground;

    //text
    public Color PrimaryText;
    public Color SecondaryText;
    public Color Title;
    public Color Header;
    public Color Span;
    public Color Caption;
    public Color Subtitle;
    public Color Hint;

    //Toggle items
    public Color ItemBase;
    public Color ItemNormal;
    public Color ItemSelected;
    public Color ItemText;
    public Color CheckboxBackground;
    public Color CheckColor;

    //buttons
    public Color ButtonBase;
    public Color ButtonNormal;
    public Color ButtonDisabled;
    public Color ButtonHover;
    public Color ButtonPressed;
    public Color ButtonSelected;
    public Color ButtonText;

    //scrollbar
    public Color scrollViewBackground;
    public Color scrollBarBase;
    public Color scrollBarNormal;
    public Color scrollBarDisabled;
    public Color scrollBarHover;
    public Color scrollBarPressed;
    public Color scrollBarSelected;
    public Color scrollBarBackground;

    //question colors
    public Color QuestionBackground;
    public Color QuestionText;

    //answer colors
    public Color AnswerBackground;
    public Color AnswerText;
    //correct answer
    public Color AnswerBackgroundCorrect;
    public Color AnswerTextCorrect;
    //wrong answer
    public Color AnswerBackgroundIncorrect;
    public Color AnswerTextIncorrect;

    //flashcards
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
