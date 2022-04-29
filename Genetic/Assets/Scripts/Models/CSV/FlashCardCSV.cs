using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>A class used for holding data from a CSV and writing it to the local database.</summary>
public class FlashcardCSV
{
    /// <summary>Gets or sets the identifier.</summary>
    /// <value>The identifier.</value>
    public int id { get; set; }
    /// <summary>Gets or sets the flashcard term (front).</summary>
    /// <value>The flashcard term.</value>
    public string term { get; set; }
    /// <summary>Gets or sets the flashcard definition (back).</summary>
    /// <value>The flashcard definition.</value>
    public string definition { get; set; }
    /// <summary>Gets or sets the image URL associated with this flashcard.</summary>
    /// <value>The flashcard image URL.</value>
    public string imageURL { get; set; }
    /// <summary>Gets or sets the tags.</summary>
    /// <value>The tags associated with this flashcard.</value>
    public string tags { get; set; }
}
