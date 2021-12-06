using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PageController : MonoBehaviour
{
    public NavigationController navigation { get => NavigationController.instance; }
    public Image swipeImage { get => GetComponent<Image>(); }
    public string pageTitle = "";
    public string pageName = "";
    public Sprite icon;
    public Action flyoutTapped { get => () => navigation.GotoPage(this); }
}
