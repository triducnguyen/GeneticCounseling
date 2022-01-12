using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint
{
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    [Indexed]
    public int QuestionID { get; set; }
    public string text { get; set; }
}
