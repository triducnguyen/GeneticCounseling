using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashCardMaster : MonoBehaviour
{
    static DBManager manager { get => DBManager.instance; }
    public Text title;
    public Text flashCardText;

    //Temp String hold answer
    private string flashcard;
    private string definition;
    
    //Check if answer is revealing
    private bool isReveal;

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
        string new_title = (!isReveal) ? "Answer: " : "Question: ";
        string currentText = (!isReveal) ? flashcard : definition;
        flashCardText.text = currentText;
        title.text = new_title;
        isReveal = !isReveal;
    }

}
