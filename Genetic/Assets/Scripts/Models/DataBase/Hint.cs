using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>A table that defines hints.</summary>
public class Hint
{
    /// <summary>Gets or sets the identifier.</summary>
    /// <value>The identifier.</value>
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    /// <summary>Gets or sets the question identifier.</summary>
    /// <value>The question identifier.</value>
    [Indexed]
    public int QuestionID { get; set; }
    /// <summary>Gets or sets the hint text.</summary>
    /// <value>The hint text.</value>
    public string text { get; set; }
}
