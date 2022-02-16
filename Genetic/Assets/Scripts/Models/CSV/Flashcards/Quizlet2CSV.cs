using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class Quizlet2CSV : MonoBehaviour
{
    public TextAsset quizlet_formatted_flashcards;

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
                cards.Add(new FlashcardCSV() { front = wordDefinition[0], back = wordDefinition[1] });
            }
        }
        ExportCSV(cards);
    }

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
