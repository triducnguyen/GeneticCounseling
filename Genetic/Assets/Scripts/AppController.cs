using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using App.Utility;
using App.Models.CSV;
using App.Themes;
using App.Models.DataBase;

/// <summary>
/// Application namespace
/// </summary>
namespace App
{
/// <summary>A singleton object that controls core aspects of the app such as theme, database, and startup.</summary>
public class AppController : Singleton<AppController>
{
    /// <summary>A reference to the local database manager.</summary>
    public DBManager manager;


    /// <summary>A list of question banks to be loaded into the local database.</summary>
    public TextAsset[] question_bank;
    /// <summary>A list of flashcards to import.</summary>
    public TextAsset[] flashcard_bank;
    /// <summary>Writes data from CSV spreadsheets into the database.</summary>
    public CSVIngress csvIngress;
        /// <summary>The quizlet converter.</summary>
        public Quizlet2CSV quizletConverter;
    /// <summary>Controls current theme and list of themes.</summary>
    public PaletteController controller;
    /// <summary>Called after object instantiation.</summary>
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
            quizletConverter.CSVfromQuizlet();
    }

    protected override void OnApplicationQuit()
    {
        //make sure db connection is closed.
        manager.Disconnect();
        //continue as normal
        base.OnApplicationQuit();
    }
}
}


