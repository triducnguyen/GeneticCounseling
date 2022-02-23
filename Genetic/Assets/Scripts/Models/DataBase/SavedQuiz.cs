using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedQuiz
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string name { get; set; }
    public string tags { get; set; }

}
