using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite;
public class Flashcard 
{
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    [Unique]
    public string text { get; set; }
}
