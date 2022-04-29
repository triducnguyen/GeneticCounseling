using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Models.DataBase
{
/// <summary>A table that represents different questions.</summary>
public class Question
{
    /// <summary>Gets or sets the identifier.</summary>
    /// <value>The identifier.</value>
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    /// <summary>Gets or sets the question text.</summary>
    /// <value>The question text.</value>
    [Unique]
    public string text { get; set; }
    /// <summary>Gets or sets the image URL.</summary>
    /// <value>The image URL.</value>
    public string imageURL { get; set; }
}

}

