using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizPage : PageController
{
    static DBManager manager { get => DBManager.instance; }

    private void Awake()
    {
        if (manager.GetAll<SavedQuiz>().Count > 0)
        {
            //make start view quiz select
            startView = views.Find((v) => v.GetType()==typeof(QuizSelect));
        }
        else
        {
            //make start view new quiz
            startView = views.Find((v) => v.GetType() == typeof(NewQuiz));
        }
        base.Awake();
    }
}
