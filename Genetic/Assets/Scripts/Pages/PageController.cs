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

    //event handlers
    public Action OnAppearing { get => () => PageAppearing() ; }
    public Action OnDisappearing { get => () => PageDisappearing(); }

    public Action<GameObject> OnViewAppearing { get => (GameObject view) => ViewAppearing(view); }
    public Action<GameObject> OnViewDisappearing { get => (GameObject view) => ViewDisappearing(view); }

    //subnavigation
    //tells page which view it should start on
    public View startView;
    //tells page which view is currently visible
    public View currentView;


    public GameObject viewContainer;
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
        //subscribe to colorschanged event
        controller.ColorsChanged += ColorsChanged;
        //set current colors
        ColorsChanged(new ColorPaletteChangedEventArgs(controller.currentPalette));
        //ensure all views are disabled
        foreach(var v in views)
        {
            v.SetActive(false);
        }
        //set current view to starting view if it exists
        if (startView != null)
        {
            GotoView(startView);
        }
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
        foreach (var h in Headers)
        {
            h.color = args.palette.Header;
        }
        foreach (var s in Spans)
        {
            s.color = args.palette.Span;
        }
        foreach (var c in Captions)
        {
            c.color = args.palette.Caption;
        }
        foreach (var su in Subtitles)
        {
            su.color = args.palette.Subtitle;
        }
        foreach (var h in Hints)
        {
            h.color = args.palette.Hint;
        }
        foreach (var p in PrimaryColors)
        {
            p.color = args.palette.Primary;
        }
        foreach (var s in SecondaryColors)
        {
            s.color = args.palette.Secondary;
        }
    }

    public void GotoView(GameObject view)
    {
        //only execute if the view given is in this page
        if (views.Contains(view))
        {
            //disable all views except given
            foreach (var v in views)
            {
                if (v == view) continue;
                if (v.activeInHierarchy)
                {
                    //let the view know it is disappearing
                    ViewDisappearing(v);
                    //deactivate view
                    v.SetActive(false);
                }
            }
            //let view know it is about to appear
            ViewAppearing(view);
            //enable selected view
            view.SetActive(true);
            //set view as current view
            currentView = view.GetComponent<View>();
        }
    }
    public void GotoView(View view)
    {
        //only execute if the view given is in this page
        if (views.Contains(view.gameObject))
        {
            //disable all views except given
            foreach (var v in views)
            {
                if (v == view.gameObject) continue;
                if (v.activeInHierarchy)
                {
                    ViewDisappearing(v);
                    v.SetActive(false);
                }
            }
            //let view know it is about to appear
            ViewAppearing(view);
            //enable selected view
            view.gameObject.SetActive(true);
            //set view as current
            currentView = view;
        }
    }

    //executed when this page is going to appear, or is going to disappear (navigated to or away from)
    protected virtual void PageAppearing()
    {
        //tell current view it is about to appear if it exists
        if (currentView != null)
        {
            ViewAppearing(currentView);
        }
    }

    protected virtual void PageDisappearing()
    {
        //tell current view it is about to disappear if it exists
        if (currentView != null)
        {
            ViewDisappearing(currentView);
        }
    }

    //executed when a view in this page is going to appear or dissapear (navigating away or to view, or navigating away or to a page)

    protected virtual void ViewAppearing(GameObject view)
    {
        //check if view is in this page
        if (views.Contains(view))
        {
            View viewComponent = view.GetComponent<View>();
            //run view specific code
            viewComponent.OnAppearing();
        }
    }

    protected virtual void ViewAppearing(View view)
    {
        //check if view is in this page
        if (views.Contains(view.gameObject))
        {
            //run view specific code
            view.OnAppearing();
        }
    }

    protected virtual void ViewDisappearing(GameObject view)
    {
        //check if view is in this page
        if (views.Contains(view))
        {
            View viewComponent = view.GetComponent<View>();
            viewComponent.OnDisappearing();
        }
    }

    protected virtual void ViewDisappearing(View view)
    {
        //check if view is in this page
        if (views.Contains(view.gameObject))
        {
            view.OnDisappearing();
        }
    }
}
