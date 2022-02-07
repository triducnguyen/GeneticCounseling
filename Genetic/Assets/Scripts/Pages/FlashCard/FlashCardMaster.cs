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
    private string temp;
    
    //Check if answer is revealing
    private bool isReveal;
    public Question currentQuestion
    {
        get
        {
            return _question;
        }
        set
        {
            _question = value;
            //set question text
            flashCardText.text = value.text;
            temp = GetAnswer().text;
            
        }
    }

    Question _question;

    Answer correctAnswer
    {
        get => GetAnswer();
    }

    Answer GetAnswer()
    {
        if (currentQuestion == null)
        {
            return null;
        }
        else
        {
            var aRelation = manager.GetItem<CorrectAnswer>(ca => ca.questionID == currentQuestion.id);
            return manager.GetItem<Answer>(a => a.id == aRelation.answerID);
        }
    }
    
    public void RevealAnswer()
    {
        string new_title = (!isReveal) ? "Answer: " : "Question: ";
        string currentText = (!isReveal) ? temp : currentQuestion.text;
        flashCardText.text = currentText;
        title.text = new_title;
        isReveal = !isReveal;
    }

}
