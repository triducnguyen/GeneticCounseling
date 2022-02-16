using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite;
public class FlashcardDefinition 
{
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    [Indexed]
    public int flashCard_id { get; set; }
    [Indexed]
    public int definition_id { get; set; }
}
