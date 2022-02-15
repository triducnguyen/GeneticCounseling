using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite;
public class FlashcardDefinition 
{
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    [Unique]
    public int flashCard_id { get; set; }
    [Unique]
    public int definition_id { get; set; }
}
