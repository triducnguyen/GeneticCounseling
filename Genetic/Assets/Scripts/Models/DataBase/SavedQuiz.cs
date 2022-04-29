using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Models.DataBase
{
/// <summary>A table that defines a saved/in-progress question set.</summary>
public class SavedQuiz
{
    /// <summary>Gets or sets the identifier.</summary>
    /// <value>The identifier.</value>
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    /// <summary>Gets or sets the set name.</summary>
    /// <value>The name.</value>
    public string name { get; set; }
    /// <summary>Gets or sets the tags associated with this set.</summary>
    /// <value>The tags.</value>
    public string tags { get; set; }

    /// <summary>Gets or sets a value indicating whether the set is in progress or not.</summary>
    /// <value>
    ///   <c>true</c> if in progress; otherwise, <c>false</c>.</value>
    public bool inProgress { get; set; }
    /// <summary>Gets or sets the question order.</summary>
    /// <value>The question order.</value>
    public string questionOrder { get; set; }
    /// <summary>Gets or sets the current question.</summary>
    /// <value>The current question.</value>
    public int currentQuestion { get; set; }

    /// <summary>Gets or sets the given answers.</summary>
    /// <value>The given answers.</value>
    public string givenAnswers { get; set; }
    /// <summary>Gets or sets the current attempt.</summary>
    /// <value>The current attempt.</value>
    public int currentAttempt { get; set; }
}

}

