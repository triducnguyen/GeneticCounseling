using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PageController : PageStyleHandler
{
    public NavigationController navigation { get => NavigationController.instance; }
    
    public Image swipeImage { get => GetComponent<Image>(); }
    public string pageTitle = "";
    public string pageName = "";
    public Sprite icon;
    public virtual Action flyoutTapped { get => () => navigation.GotoPage(this); }

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
    public List<View> views;
    public List<GameObject> viewObjects
    {
        get
        {
            return views.Select((v) => v.gameObject).ToList();
        }
    }
    

    protected virtual void Awake()
    {
        base.Awake();
        //ensure all views are disabled
        foreach (var v in viewObjects)
        {
            v.SetActive(false);
        }
        //set current view to starting view if it exists
        if (startView != null)
        {
            GotoView(startView);
        }
    }

    

    public void GotoView(GameObject viewObject)
    {
        //only execute if the view given is in this page
        if (viewObjects.Contains(viewObject)) {

            View viewComponent = viewObject.GetComponent<View>();
            //disable all views except given
            foreach (var v in views)
            {
                if (v == viewComponent) continue;
                if (v.gameObject.activeInHierarchy)
                {
                    //let the view know it is disappearing
                    ViewDisappearing(v);
                    //deactivate view
                    v.gameObject.SetActive(false);
                }
            }
            //let view know it is about to appear
            ViewAppearing(viewComponent);
            //enable selected view
            viewObject.SetActive(true);
            //set view as current view
            currentView = viewComponent;
        }
    }
    public void GotoView(View view)
    {
        //only execute if the view given is in this page
        if (views.Contains(view))
        {
            //disable all views except given
            foreach (var v in views)
            {
                if (v == view.gameObject) continue;
                if (v.gameObject.activeInHierarchy)
                {
                    ViewDisappearing(v);
                    v.gameObject.SetActive(false);
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
        if (viewObjects.Contains(view))
        {
            View viewComponent = view.GetComponent<View>();
            //run view specific code
            viewComponent.OnAppearing();
        }
    }

    protected virtual void ViewAppearing(View view)
    {
        //check if view is in this page
        if (viewObjects.Contains(view.gameObject))
        {
            //run view specific code
            view.OnAppearing();
        }
    }

    protected virtual void ViewDisappearing(GameObject view)
    {
        //check if view is in this page
        if (viewObjects.Contains(view))
        {
            View viewComponent = view.GetComponent<View>();
            viewComponent.OnDisappearing();
        }
    }

    protected virtual void ViewDisappearing(View view)
    {
        //check if view is in this page
        if (viewObjects.Contains(view.gameObject))
        {
            view.OnDisappearing();
        }
    }
}
