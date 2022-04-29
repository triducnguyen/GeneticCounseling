using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Defines a color palette.</summary>
public class ColorPalette : MonoBehaviour
{

    //main colors
    /// <summary>The primary
    /// color of the app.</summary>
    public Color Primary;
    /// <summary>The secondary
    /// color of the app.</summary>
    public Color Secondary;
    /// <summary>The third
    /// color of the app.</summary>
    public Color Tertiary;
    /// <summary>The fourth
    /// color of the app</summary>
    public Color Quaternary;

    //navigation bar
    /// <summary>The navbar
    /// color.</summary>
    public Color Navbar;

    //flyout menu
    /// <summary>The flyout button color.</summary>
    public Color FlyoutBtn;
    /// <summary>The flyout touch background
    /// color</summary>
    public Color FlyoutTouchBackground;
    /// <summary>The flyout header background
    /// color.</summary>
    public Color FlyoutHeaderBackground;
    /// <summary>The flyout background
    /// color.</summary>
    public Color FlyoutBackground;
    /// <summary>The flyout item background
    /// color.</summary>
    public Color FlyoutItemBackground;
    /// <summary>The flyout text
    /// color.</summary>
    public Color FlyoutText;
    /// <summary>The flyout icon
    /// color.</summary>
    public Color FlyoutIcon;
    /// <summary>The flyout footer background
    /// color.</summary>
    public Color FlyoutFooterBackground;

    //pages
    /// <summary>The page background
    /// color.</summary>
    public Color PageBackground;

    //text
    /// <summary>The primary text
    /// color.</summary>
    public Color PrimaryText;
    /// <summary>The secondary text
    /// color.</summary>
    public Color SecondaryText;
    /// <summary>The title
    /// color.</summary>
    public Color Title;
    /// <summary>The header
    /// color</summary>
    public Color Header;
    /// <summary>The span
    /// color.</summary>
    public Color Span;
    /// <summary>The caption
    /// color.</summary>
    public Color Caption;
    /// <summary>The subtitle
    /// color.</summary>
    public Color Subtitle;
    /// <summary>The hint
    /// color.</summary>
    public Color Hint;

    //Toggle items
    /// <summary>The item base
    /// color.</summary>
    public Color ItemBase;
    /// <summary>The item's normal
    /// color.</summary>
    public Color ItemNormal;
    /// <summary>The item's selected
    /// color.</summary>
    public Color ItemSelected;
    /// <summary>The item's text
    /// color.</summary>
    public Color ItemText;
    /// <summary>The checkbox background
    /// color.</summary>
    public Color CheckboxBackground;
    /// <summary>The checkbox check color.</summary>
    public Color CheckColor;

    //buttons
    /// <summary>The button base
    /// color.</summary>
    public Color ButtonBase;
    /// <summary>The button's normal
    /// color.</summary>
    public Color ButtonNormal;
    /// <summary>The button's disabled
    /// color.</summary>
    public Color ButtonDisabled;
    /// <summary>The button's hover
    /// color.</summary>
    public Color ButtonHover;
    /// <summary>The button's pressed
    /// color.</summary>
    public Color ButtonPressed;
    /// <summary>The button's selected
    /// color.</summary>
    public Color ButtonSelected;
    /// <summary>The button's text
    /// color.</summary>
    public Color ButtonText;

    //scrollbar
    /// <summary>The scroll view background
    /// color.</summary>
    public Color scrollViewBackground;
    /// <summary>The scroll bar base
    /// color.</summary>
    public Color scrollBarBase;
    /// <summary>The scroll bar's normal
    /// color.</summary>
    public Color scrollBarNormal;
    /// <summary>The scroll bar's disabled
    /// color.</summary>
    public Color scrollBarDisabled;
    /// <summary>The scroll bar's hover
    /// color.</summary>
    public Color scrollBarHover;
    /// <summary>The scroll bar's pressed
    /// color.</summary>
    public Color scrollBarPressed;
    /// <summary>The scroll bar's selected
    /// color.</summary>
    public Color scrollBarSelected;
    /// <summary>The scroll bar's background
    /// color.</summary>
    public Color scrollBarBackground;

    //question colors
    /// <summary>The question background
    /// color.</summary>
    public Color QuestionBackground;
    /// <summary>The question text
    /// color.</summary>
    public Color QuestionText;

    //answer colors
    /// <summary>The answer background
    /// color.</summary>
    public Color AnswerBackground;
    /// <summary>The answer text
    /// color.</summary>
    public Color AnswerText;
    //correct answer
    /// <summary>The answer background correct
    /// color.</summary>
    public Color AnswerBackgroundCorrect;
    /// <summary>The answer text correct
    /// color.</summary>
    public Color AnswerTextCorrect;
    //wrong answer
    /// <summary>The answer background incorrect
    /// color.</summary>
    public Color AnswerBackgroundIncorrect;
    /// <summary>The answer text incorrect
    /// color.</summary>
    public Color AnswerTextIncorrect;

    //flashcards
    /// <summary>The flashcard background
    /// color.</summary>
    public Color FlashcardBackground;
    /// <summary>The flashcard text color.</summary>
    public Color FlashcardText;

    /// <summary>Highlights the specified color.</summary>
    /// <param name="color">The color to highlight.</param>
    /// <returns>Highlighted color.</returns>
    public static Color Highlight(Color color)
    {
        //add small amount to each channel
        return Highlight(color, .01f);
    }

    /// <summary>Highlights the specified color by a float amount.</summary>
    /// <param name="color">The color to be highlighted.</param>
    /// <param name="amount">The amount to highlight by.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public static Color Highlight(Color color, float amount)
    {
        color.r += amount;
        color.g += amount;
        color.b += amount;
        return color;
    }

    /// <summary>Shadows the specified color.</summary>
    /// <param name="color">The color to darken.</param>
    /// <returns>Darkened color.</returns>
    public static Color Shadow(Color color)
    {
        return Shadow(color, .01f);
    }

    /// <summary>Shadows the specified color by a float amount.</summary>
    /// <param name="color">The color to be darkened.</param>
    /// <param name="amount">The amount to darken by.</param>
    /// <returns>Darkened color.</returns>
    public static Color Shadow(Color color, float amount)
    {
        return Highlight(color, -amount);
    }

}
/// <summary>Custom argument class to contain information about what palette was swapped to.</summary>
public class ColorPaletteChangedEventArgs : EventArgs
{
    /// <summary>Gets the color palette that was changed to.</summary>
    /// <value>The color palette.</value>
    public ColorPalette palette { get => _palette; }
    /// <summary>The palette's private value.</summary>
    private ColorPalette _palette;
    /// <summary>Initializes a new instance of the <see cref="ColorPaletteChangedEventArgs" /> class.</summary>
    /// <param name="p">The palette that was swapped to.</param>
    public ColorPaletteChangedEventArgs(ColorPalette p)
    {
        _palette = p;
    }
}
