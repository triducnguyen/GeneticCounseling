using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizItem : MonoBehaviour
{
    public Text name;
    public Toggle toggle;
    public SavedQuiz quiz
    {
        get => _q;
        set
        {
            _q = value;
            name.text = _q.name;
        }
    }
    SavedQuiz _q;
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
