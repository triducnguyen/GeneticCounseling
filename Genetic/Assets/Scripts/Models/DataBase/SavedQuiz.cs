using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedQuiz
{
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    public string name { get; set; }
    public string tags { get; set; }

    public bool inProgress { get; set; }
    public string questionOrder { get; set; }
    public int currentQuestion { get; set; }

    public string givenAnswers { get; set; }
    public int currentAttempt { get; set; }
}
