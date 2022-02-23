using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizItem : MonoBehaviour
{
    public Toggle toggle;
    public SavedQuiz quiz;
    public Image image;
    public Color original;

    public void OnChange()
    {
        if (toggle.isOn)
        {
            //set selected
            image.color = AppController.instance.controller.currentPalette.SelectedItem;
        }
        else
        {
            //unset selected
            image.color = original;
        }
    }
}
