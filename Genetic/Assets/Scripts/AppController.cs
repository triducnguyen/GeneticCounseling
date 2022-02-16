using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AppController : Singleton<AppController>
{
    public DBManager manager;


    public TextAsset[] question_bank;
    public TextAsset[] flashcard_bank;
    public CSVIngress csvIngress;

    public List<QuestionCSV> questionList;
    public PaletteController controller;
    protected override void Awake()
    {
        base.Awake();
        //Ensure object is not destroyed
        DontDestroyOnLoad(gameObject);
        //Load questions
        foreach (var csv in question_bank)
        {
            csvIngress.ImportAnswerSheet(csv);
        }
        foreach(var csv in flashcard_bank)
        {
            csvIngress.ImportFlashcardSheet(csv);
        }
    }
}
