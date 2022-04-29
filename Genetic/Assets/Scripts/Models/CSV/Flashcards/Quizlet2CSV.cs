using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

/// <summary>A class for importing quizlet flashcard sets. Be sure to use '%*%' as the card separator, and '%!%' as the front/back separator.</summary>
public class Quizlet2CSV : MonoBehaviour
{
    public TextAsset quizlet_formatted_flashcards;

    /// <summary>Generates a CSV file from quizlet flashcard set.</summary>
    void CSVfromQuizlet()
    {
        List<FlashcardCSV> cards = new List<FlashcardCSV>();
        string[] separator = { "%*%" };
        //turn contents of string into flashcards
        List<string> flashcards = quizlet_formatted_flashcards.text.Split(separator, System.StringSplitOptions.RemoveEmptyEntries).ToList();
        separator = new string[] { "%!%" };
        foreach (var fc in flashcards)
        {
            var wordDefinition = fc.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (wordDefinition.Count > 2)
            {
                //theres an issue with the formatting. There should only be two values.
            }
            else
            {
                cards.Add(new FlashcardCSV() { term = wordDefinition[0], definition = wordDefinition[1] });
            }
        }
        ExportCSV(cards);
    }

    /// <summary>Exports the CSV file.</summary>
    /// <param name="cards">The cards to export.</param>
    void ExportCSV(List<FlashcardCSV> cards)
    {
        var path = new Uri(Path.Combine(Application.persistentDataPath, "exported_cards.csv"));
        Debug.Log("Saving flashcards to "+path.AbsoluteUri);
        using (var writer = new StreamWriter(path.AbsolutePath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(cards);
        }
        Debug.Log("Saved flashcards to "+path.AbsolutePath);
    }
}
