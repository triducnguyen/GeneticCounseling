using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionTag
{
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    [Indexed]
    public int questionID { get; set; }
    public int tagID { get; set; }
}
