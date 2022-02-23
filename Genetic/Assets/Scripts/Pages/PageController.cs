using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PageController : MonoBehaviour
{
    public NavigationController navigation { get => NavigationController.instance; }
    public PaletteController controller { get => AppController.instance.controller; }
    public Image swipeImage { get => GetComponent<Image>(); }
    public string pageTitle = "";
    public string pageName = "";
    public Sprite icon;
    public Action flyoutTapped { get => () => navigation.GotoPage(this); }
    

    public GameObject foreground;
    public GameObject background;
    public List<GameObject> views;
    public Image backgroundImage;
    public List<Image> PrimaryColors;
    public List<Image> SecondaryColors;
    public List<Text> PrimaryTexts;
    public List<Text> SecondaryTexts;
    public List<Text> Titles;
    public List<Text> Headers;
    public List<Text> Spans;
    public List<Text> Captions;
    public List<Text> Subtitles;
    public List<Text> Hints;

    protected virtual void Start()
    {
        controller.ColorsChanged += ColorsChanged;
        ColorsChanged(new ColorPaletteChangedEventArgs(controller.currentPalette));
    }

    public virtual void ColorsChanged(ColorPaletteChangedEventArgs args)
    {
        backgroundImage.color = args.palette.PageBackground;
        //set all colors
        foreach (var pt in PrimaryTexts)
        {
            pt.color = args.palette.PrimaryText;
        }
        foreach (var pt in SecondaryTexts)
        {
            pt.color = args.palette.SecondaryText;
        }
        foreach (var t in Titles)
        {
            t.color = args.palette.Title;
        }
        foreach(var h in Headers)
        {
            h.color = args.palette.Header;
        }
        foreach(var s in Spans)
        {
            s.color = args.palette.Span;
        }
        foreach (var c in Captions)
        {
            c.color = args.palette.Caption;
        }
        foreach(var su in Subtitles)
        {
            su.color = args.palette.Subtitle;
        }
        foreach(var h in Hints)
        {
            h.color = args.palette.Hint;
        }
        foreach(var p in PrimaryColors)
        {
            p.color = args.palette.Primary;
        }
        foreach(var s in SecondaryColors)
        {
            s.color = args.palette.Secondary;
        }
    }

    public void ShowView(GameObject view)
    {
        //disable all views except given
        foreach (var v in views)
        {
            if (v == view) continue;
            v.SetActive(false);
        }
        //enable selected view
        view.SetActive(true);
    }
}
