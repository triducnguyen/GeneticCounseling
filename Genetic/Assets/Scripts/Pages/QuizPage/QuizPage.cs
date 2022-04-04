using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizPage : PageController
{
    static DBManager manager { get => DBManager.instance; }

    public View newQuizView;
    public View quizSelectView;

    public Image QuestionBackground;
    public Image[] AnswersBackground;

    public Text QuestionText;
    public Text[] AnswersText;

    private void Awake()
    {
        if (manager.GetAll<SavedQuiz>().Count > 0)
        {
            //make start view quiz select
            startView = quizSelectView;
        }
        else
        {
            //make start view new quiz
            startView = newQuizView;
        }
    }

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
