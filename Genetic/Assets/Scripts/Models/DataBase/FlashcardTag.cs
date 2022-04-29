using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite;
/// <summary>A table that defines a relation between a flashcard and a tag.</summary>
public class FlashcardTag
{
    /// <summary>Gets or sets the identifier.</summary>
    /// <value>The identifier.</value>
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    /// <summary>Gets or sets the tag identifier.</summary>
    /// <value>The tag identifier.</value>
    [Indexed]
    public int tagID { get; set; }
    /// <summary>Gets or sets the flashcard identifier.</summary>
    /// <value>The flashcard identifier.</value>
    [Indexed]
    public int flashcardID { get; set; }
}
