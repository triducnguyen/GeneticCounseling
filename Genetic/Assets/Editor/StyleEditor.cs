using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColorPalette))]
public class ThemeEditor : Editor
{
    //main colors
    SerializedProperty Primary;
    SerializedProperty Secondary;
    SerializedProperty Tertiary;
    SerializedProperty Quaternary;

    //navigation bar
    SerializedProperty Navbar;

    //flyout menu
    SerializedProperty FlyoutBtn;
    SerializedProperty FlyoutTouchBackground;
    SerializedProperty FlyoutHeaderBackground;
    SerializedProperty FlyoutBackground;
    SerializedProperty FlyoutItemBackground;
    SerializedProperty FlyoutText;
    SerializedProperty FlyoutIcon;
    SerializedProperty FlyoutFooterBackground;

    //pages
    SerializedProperty PageBackground;
    SerializedProperty PageForeground;

    //text
    SerializedProperty PrimaryText;
    SerializedProperty SecondaryText;
    SerializedProperty Title;
    SerializedProperty Header;
    SerializedProperty Span;
    SerializedProperty Caption;
    SerializedProperty Subtitle;
    SerializedProperty Hint;

    //toggle items
    SerializedProperty ItemBase;
    SerializedProperty ItemNormal;
    SerializedProperty ItemSelected;
    SerializedProperty ItemText;
    SerializedProperty CheckboxBackground;
    SerializedProperty CheckColor;

    //buttons
    SerializedProperty ButtonBase;
    SerializedProperty ButtonNormal;
    SerializedProperty ButtonDisabled;
    SerializedProperty ButtonHover;
    SerializedProperty ButtonPressed;
    SerializedProperty ButtonSelected;
    SerializedProperty ButtonText;

    //scrollbar
    SerializedProperty scrollViewBackground;
    SerializedProperty scrollBarBase;
    SerializedProperty scrollBarNormal;
    SerializedProperty scrollBarDisabled;
    SerializedProperty scrollBarHover;
    SerializedProperty scrollBarPressed;
    SerializedProperty scrollBarSelected;
    SerializedProperty scrollBarBackground;

    //question colors
    SerializedProperty QuestionBackground;
    SerializedProperty QuestionText;

    //answer colors
    SerializedProperty AnswerBackground;
    SerializedProperty AnswerText;
    //correct answer
    SerializedProperty AnswerBackgroundCorrect;
    SerializedProperty AnswerTextCorrect;
    //wrong answer
    SerializedProperty AnswerBackgroundIncorrect;
    SerializedProperty AnswerTextIncorrect;

    //flashcards
    SerializedProperty FlashcardBackground;
    SerializedProperty FlashcardText;

    //bools for showing groups
    bool mainColors, navBar, flyout, pages, toggle, text, button, scroll, question, answers, flashcards = false;

    private void OnEnable()
    {
         Primary = serializedObject.FindProperty("Primary");
         Secondary = serializedObject.FindProperty("Secondary");
         Tertiary = serializedObject.FindProperty("Tertiary");
         Quaternary = serializedObject.FindProperty("Quaternary");

        //navigation bar
         Navbar = serializedObject.FindProperty("Navbar");

        //flyout menu
         FlyoutBtn = serializedObject.FindProperty("FlyoutBtn");
         FlyoutTouchBackground = serializedObject.FindProperty("FlyoutTouchBackground");
         FlyoutHeaderBackground = serializedObject.FindProperty("FlyoutHeaderBackground");
         FlyoutBackground = serializedObject.FindProperty("FlyoutBackground");
         FlyoutItemBackground = serializedObject.FindProperty("FlyoutItemBackground");
         FlyoutText = serializedObject.FindProperty("FlyoutText");
         FlyoutIcon = serializedObject.FindProperty("FlyoutIcon");
         FlyoutFooterBackground = serializedObject.FindProperty("FlyoutFooterBackground");

        //pages
         PageBackground = serializedObject.FindProperty("PageBackground");
         PageForeground = serializedObject.FindProperty("PageBackground");

        //text
         PrimaryText = serializedObject.FindProperty("PrimaryText");
         SecondaryText = serializedObject.FindProperty("SecondaryText");
         Title = serializedObject.FindProperty("Title");
         Header = serializedObject.FindProperty("Header");
         Span = serializedObject.FindProperty("Span");
         Caption = serializedObject.FindProperty("Caption");
         Subtitle = serializedObject.FindProperty("Subtitle");
         Hint = serializedObject.FindProperty("Hint");

        //toggle items
         ItemBase = serializedObject.FindProperty("ItemBase");
         ItemNormal = serializedObject.FindProperty("ItemNormal");
         ItemSelected = serializedObject.FindProperty("ItemSelected");
         ItemText = serializedObject.FindProperty("ItemText");
         CheckboxBackground = serializedObject.FindProperty("CheckboxBackground");
         CheckColor = serializedObject.FindProperty("CheckColor");

        //button
        ButtonBase = serializedObject.FindProperty("ButtonBase");
        ButtonNormal = serializedObject.FindProperty("ButtonNormal");
        ButtonDisabled = serializedObject.FindProperty("ButtonDisabled");
        ButtonHover = serializedObject.FindProperty("ButtonHover");
        ButtonPressed = serializedObject.FindProperty("ButtonPressed");
        ButtonSelected = serializedObject.FindProperty("ButtonSelected");
        ButtonText = serializedObject.FindProperty("ButtonText");

        //scrollbar
        scrollViewBackground = serializedObject.FindProperty("scrollViewBackground");
        scrollBarBase = serializedObject.FindProperty("scrollBarBase");
        scrollBarNormal = serializedObject.FindProperty("scrollBarNormal");
        scrollBarDisabled = serializedObject.FindProperty("scrollBarDisabled");
        scrollBarHover = serializedObject.FindProperty("scrollBarHover");
        scrollBarPressed = serializedObject.FindProperty("scrollBarPressed");
        scrollBarSelected = serializedObject.FindProperty("scrollBarSelected");
        scrollBarBackground = serializedObject.FindProperty("scrollBarBackground");

        //question colors
         QuestionBackground = serializedObject.FindProperty("QuestionBackground");
         QuestionText = serializedObject.FindProperty("QuestionText");

        //answer colors
         AnswerBackground = serializedObject.FindProperty("AnswerBackground");
         AnswerText = serializedObject.FindProperty("AnswerText");
        //correct answer
         AnswerBackgroundCorrect = serializedObject.FindProperty("AnswerBackgroundCorrect");
         AnswerTextCorrect = serializedObject.FindProperty("AnswerTextCorrect");
        //wrong answer
         AnswerBackgroundIncorrect = serializedObject.FindProperty("AnswerBackgroundIncorrect");
         AnswerTextIncorrect = serializedObject.FindProperty("AnswerTextIncorrect");

        //flashcards
         FlashcardBackground = serializedObject.FindProperty("FlashcardBackground");
         FlashcardText = serializedObject.FindProperty("FlashcardText");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        mainColors = EditorGUILayout.Foldout(mainColors, "Main Colors");
        if (mainColors)
        {
            EditorGUILayout.PropertyField(Primary);
            EditorGUILayout.PropertyField(Secondary);
            EditorGUILayout.PropertyField(Tertiary);
            EditorGUILayout.PropertyField(Quaternary);
        }

        navBar = EditorGUILayout.Foldout(navBar, "Navbar");
        if (navBar)
        {
            EditorGUILayout.PropertyField(Navbar);
        }

        flyout = EditorGUILayout.Foldout(flyout, "Flyout Menu");
        if (flyout)
        {
            EditorGUILayout.PropertyField(FlyoutBtn);
            EditorGUILayout.PropertyField(FlyoutTouchBackground);
            EditorGUILayout.PropertyField(FlyoutHeaderBackground);
            EditorGUILayout.PropertyField(FlyoutBackground);
            EditorGUILayout.PropertyField(FlyoutItemBackground);
            EditorGUILayout.PropertyField(FlyoutText);
            EditorGUILayout.PropertyField(FlyoutIcon);
            EditorGUILayout.PropertyField(FlyoutFooterBackground);
        }

        pages = EditorGUILayout.Foldout(pages, "Pages");
        if (pages)
        {
            EditorGUILayout.PropertyField(PageBackground);
        }

        text = EditorGUILayout.Foldout(text, "Text");
        if (text)
        {
            EditorGUILayout.PropertyField(PrimaryText);
            EditorGUILayout.PropertyField(SecondaryText);
            EditorGUILayout.PropertyField(Title);
            EditorGUILayout.PropertyField(Header);
            EditorGUILayout.PropertyField(Span);
            EditorGUILayout.PropertyField(Caption);
            EditorGUILayout.PropertyField(Subtitle);
            EditorGUILayout.PropertyField(Hint);
        }

        toggle = EditorGUILayout.Foldout(toggle, "Toggle");
        if (toggle)
        {
            EditorGUILayout.PropertyField(ItemBase);
            EditorGUILayout.PropertyField(ItemNormal);
            EditorGUILayout.PropertyField(ItemSelected);
            EditorGUILayout.PropertyField(ItemText);
            EditorGUILayout.PropertyField(CheckboxBackground);
            EditorGUILayout.PropertyField(CheckColor);
        }

        button = EditorGUILayout.Foldout(button, "Button");
        if (button)
        {
            EditorGUILayout.PropertyField(ButtonBase);
            EditorGUILayout.PropertyField(ButtonNormal);
            EditorGUILayout.PropertyField(ButtonDisabled);
            EditorGUILayout.PropertyField(ButtonHover);
            EditorGUILayout.PropertyField(ButtonPressed);
            EditorGUILayout.PropertyField(ButtonSelected);
            EditorGUILayout.PropertyField(ButtonText);
        }

        scroll = EditorGUILayout.Foldout(scroll, "Scroll View");
        if (scroll)
        {
            EditorGUILayout.PropertyField(scrollViewBackground);
            EditorGUILayout.PropertyField(scrollBarBase);
            EditorGUILayout.PropertyField(scrollBarNormal);
            EditorGUILayout.PropertyField(scrollBarDisabled);
            EditorGUILayout.PropertyField(scrollBarHover);
            EditorGUILayout.PropertyField(scrollBarPressed);
            EditorGUILayout.PropertyField(scrollBarSelected);
            EditorGUILayout.PropertyField(scrollBarBackground);
        }

        question = EditorGUILayout.Foldout(question, "Question");
        if (question)
        {
            EditorGUILayout.PropertyField(QuestionBackground);
            EditorGUILayout.PropertyField(QuestionText);
        }

        answers = EditorGUILayout.Foldout(answers, "Answers");
        if (answers)
        {
            EditorGUILayout.PropertyField(AnswerBackground);
            EditorGUILayout.PropertyField(AnswerText);
            EditorGUILayout.PropertyField(AnswerBackgroundCorrect);
            EditorGUILayout.PropertyField(AnswerTextCorrect);
            EditorGUILayout.PropertyField(AnswerBackgroundIncorrect);
            EditorGUILayout.PropertyField(AnswerTextIncorrect);
        }

        flashcards = EditorGUILayout.Foldout(flashcards, "Flashcard");
        if (flashcards)
        {
            EditorGUILayout.PropertyField(FlashcardBackground);
            EditorGUILayout.PropertyField(FlashcardText);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
