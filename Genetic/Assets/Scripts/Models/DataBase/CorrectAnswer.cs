using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>A table of all the correct answers. Describes the relation between a questionID and an answerID.</summary>
public class CorrectAnswer 
{
    /// <summary>Gets or sets the identifier.</summary>
    /// <value>The identifier.</value>
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    /// <summary>Gets or sets the question identifier.</summary>
    /// <value>The question identifier.</value>
    [Indexed]
    public int questionID { get; set; }
    /// <summary>Gets or sets the answer identifier.</summary>
    /// <value>The answer identifier.</value>
    [Indexed]
    public int answerID { get; set; }

}
