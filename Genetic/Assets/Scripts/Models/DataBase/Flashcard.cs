using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite;
/// <summary>A table that defines a flashcard.</summary>
public class Flashcard 
{
    /// <summary>Gets or sets the flashcard identifier.</summary>
    /// <value>The identifier.</value>
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    /// <summary>Gets or sets the flashcard term.</summary>
    /// <value>The flashcard term.</value>
    public string term { get; set; }
    /// <summary>Gets or sets the definition.</summary>
    /// <value>The flashcard definition.</value>
    public string definition { get; set; }
    
}
