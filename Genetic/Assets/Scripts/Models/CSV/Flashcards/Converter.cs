using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Converter : MonoBehaviour
{
    public TextAsset quizlet_formatted_flashcards;
    // Start is called before the first frame update
    void Start()
    {
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
                //check if card already exists
                    //if card does exist, update it with new information
                    //if card doesnt exist, create a new one and save it
            }
        }
    }
}
