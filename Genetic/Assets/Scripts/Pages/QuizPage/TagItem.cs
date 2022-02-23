using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagItem : MonoBehaviour
{
    public Text tagText;
    public Toggle toggle;

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