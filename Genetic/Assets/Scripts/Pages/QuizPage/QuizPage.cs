using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizPage : PageController
{
    static DBManager manager { get => DBManager.instance; }

    public View quizView;
    public View newQuizView;
    public View quizSelectView;

    

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
        base.Awake();
    }
}
