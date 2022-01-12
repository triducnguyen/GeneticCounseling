using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerTag
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }
    [Indexed]
    public int tagID { get; set; }
    [Indexed]
    public int answerID { get; set; }
}