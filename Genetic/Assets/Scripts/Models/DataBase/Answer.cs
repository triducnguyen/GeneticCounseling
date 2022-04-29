using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Models.DataBase
{
    /// <summary>Represents a table of different answers, right and wrong.</summary>
    public class Answer
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [AutoIncrement, PrimaryKey]
        public int id { get; set; }
        /// <summary>Gets or sets the answer text.</summary>
        /// <value>The answer text.</value>
        [Unique]
        public string text { get; set; }
    }
}