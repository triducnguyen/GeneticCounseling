using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizPage : PageController
{
    public Image QuestionBackground;
    public Image[] AnswersBackground;

    public Text QuestionText;
    public Text[] AnswersText;

    public override void ColorsChanged(ColorPaletteChangedEventArgs args)
    {
        base.ColorsChanged(args);
        QuestionBackground.color = args.palette.QuestionBackground;
        QuestionText.color = args.palette.QuestionText;
        foreach (var b in AnswersBackground)
        {
            b.color = args.palette.AnswerBackground;
        }
        foreach (var a in AnswersText)
        {
            a.color = args.palette.AnswerText;
        }

    }
}
