using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    public NavigationController navigation { get => NavigationController.instance; }
    public string pageTitle = "";
    public string pageName = "";
    public Sprite icon;
    public Action flyoutTapped { get => () => navigation.GotoPage(this); }
}
