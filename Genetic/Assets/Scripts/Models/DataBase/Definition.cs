using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite;

public class Definition 
{
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    public string text { get; set; }
}
