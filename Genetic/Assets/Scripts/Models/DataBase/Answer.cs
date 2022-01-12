using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer
{
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    [Unique]
    public string text { get; set; }
}
