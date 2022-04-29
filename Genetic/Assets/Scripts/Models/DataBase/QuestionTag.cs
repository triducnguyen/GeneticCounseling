using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Models.DataBase
{
/// <summary>A table that defines a relation between a question and a tag.</summary>
public class QuestionTag
{
    /// <summary>Gets or sets the identifier.</summary>
    /// <value>The identifier.</value>
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    /// <summary>Gets or sets the question identifier.</summary>
    /// <value>The question identifier.</value>
    [Indexed]
    public int questionID { get; set; }
    /// <summary>Gets or sets the tag identifier.</summary>
    /// <value>The tag identifier.</value>
    public int tagID { get; set; }
}

}

