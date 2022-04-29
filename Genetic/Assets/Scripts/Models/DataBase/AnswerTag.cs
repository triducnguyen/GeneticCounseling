using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Models.DataBase
{
/// <summary>A table describing a tagID associated with an answerID.</summary>
public class AnswerTag
{
    /// <summary>Gets or sets the identifier.</summary>
    /// <value>The identifier.</value>
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }
    /// <summary>Gets or sets the tag identifier.</summary>
    /// <value>The tag identifier.</value>
    [Indexed]
    public int tagID { get; set; }
    /// <summary>Gets or sets the answer identifier.</summary>
    /// <value>The answer identifier.</value>
    [Indexed]
    public int answerID { get; set; }

}
}