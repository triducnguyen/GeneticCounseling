using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite;
public class FlashcardTag
{
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    [Unique]
    public int tag_id { get; set; }
    [Unique]
    public int flashCardDefinition_id { get; set; }
}
