using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlashCardMaster : MonoBehaviour
{
    static DBManager manager { get => DBManager.instance; }
    public TMP_Text title;
    public TMP_Text flashCardText;

    //Temp String hold answer
    private string flashcard;
    private string definition;
    
    //Check if answer is revealing
    private bool isReveal;
    public void Start()
    {
        flashCardText.text = flashcard;
        
    }

    public string FlashCard
    {
        get => flashcard;
        set
        {
            flashcard = value;
        }
    }

    public string Definition
    {
        get => definition;
        set
        {
            definition = value;
        }
    }


    
    public void RevealAnswer()
    {
        string new_title = (!isReveal) ? "Definition: " : "Term: ";
        string currentText = (!isReveal) ? definition : flashcard;
        flashCardText.text = currentText;
        title.text = new_title;
        isReveal = !isReveal;
    }

}
