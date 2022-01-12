using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncorrectAnswer
{
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    [Indexed]
    public int questionID { get; set; }
    [Indexed]
    public int answerID { get; set; }
}
