using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyoutItemController : MonoBehaviour
{
    public string pageName = "";
    public Text title;
    public Image icon;
    public Button button;
    public Action action;

    public void OnTap()
    {
        action.Invoke();
    }
}
