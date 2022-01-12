using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tag
{
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    [Unique]
    public string tag { get; set; }
}
