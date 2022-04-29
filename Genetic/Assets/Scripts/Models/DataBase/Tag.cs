using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Models.DataBase
{
/// <summary>Represents a table of different tags.</summary>
public class Tag
{
    /// <summary>Gets or sets the identifier.</summary>
    /// <value>The identifier.</value>
    [AutoIncrement, PrimaryKey]
    public int id { get; set; }
    /// <summary>Gets or sets the tag text.</summary>
    /// <value>The tag.</value>
    [Unique]
    public string tag { get; set; }
}

}

