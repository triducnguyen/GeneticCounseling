using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Represents a question in a question bank CSV table.</summary>
public class QuestionCSV
{
    /// <summary>Gets or sets the identifier.</summary>
    /// <value>The identifier.</value>
    public int ID { get; set; }
    /// <summary>Gets or sets the question text.</summary>
    /// <value>The question text.</value>
    public string Question { get; set; }
    /// <summary>Gets or sets the correct answer text.</summary>
    /// <value>The correct answer text.</value>
    public string CorrectAnswer { get; set; }
    /// <summary>Gets or sets answer1.</summary>
    /// <value>Incorrect answer 1.</value>
    public string Answer1 { get; set; }
    /// <summary>Gets or sets answer2.</summary>
    /// <value>Incorrect answer 2.</value>
    public string Answer2 { get; set; }
    /// <summary>Gets or sets answer3.</summary>
    /// <value>Incorrect answer 3.</value>
    public string Answer3 { get; set; }
    /// <summary>Gets or sets answer4.</summary>
    /// <value>Incorrect answer 4.</value>
    public string Answer4 { get; set; }
    /// <summary>Gets or sets answer5.</summary>
    /// <value>Incorrect answer 5.</value>
    public string Answer5 { get; set; }
    /// <summary>Gets or sets the explanation text.</summary>
    /// <value>The Explanation text.</value>
    public string Explanation { get; set; }
    /// <summary>Gets or sets the image url.</summary>
    /// <value>The image url.</value>
    public string ImageURL { get; set; }
    /// <summary>Gets or sets the tags.</summary>
    /// <value>The tags associated with this question.</value>
    public string Tags { get; set; } 
}
