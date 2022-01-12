using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    [Unique]
    public string text { get; set; }
    public string imageURL { get; set; }
}
