using App.Navigation;
using App.Pages;
using App.Themes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Pages
{
/// <summary>The base class of a page. Controls all base functions of a class, such as navigation and palette changes.</summary>
public class PageController : PageStyleHandler
{
    /// <summary>Gets the navigation controller.</summary>
    /// <value>The navigation controller.</value>
    public NavigationController navigation { get => NavigationController.instance; }

    /// <summary>The display title for the page.</summary>
    public string pageTitle = "";
    /// <summary>The page name. Should be unique because it is used to identify pages.</summary>
    public string pageName = "";
    /// <summary>The icon
    /// to represent this page in the flyout menu.</summary>
    public Sprite icon;
    /// <summary>The action to perform when this page's flyout item is tapped.
    /// Change this if you want alter the behavior of the button.</summary>
    /// <value>The action to perform.</value>
    public virtual Action flyoutTapped { get => () => navigation.GotoPage(this); }

    //event handlers
    /// <summary>The action to perform when the page appears. Default is just to call 'PageAppearing'.</summary>
    /// <value>The action to perform.</value>
    public virtual Action OnAppearing { get => () => PageAppearing() ; }
    /// <summary>The action to perform when the page disappears.</summary>
    /// <value>The action to perform.</value>
    public virtual Action OnDisappearing { get => () => PageDisappearing(); }

    //subnavigation
    //tells page which view it should start on
    /// <summary>The view to switch to when the page is first navigated to.</summary>
    public View startView;
    //tells page which view is currently visible
    /// <summary>The current view
    /// the page is displaying.</summary>
    public View currentView;

    /// <summary>The views
    /// in this page.</summary>
    public List<View> views;
    /// <summary>Gets a list of all of the views' gameobjects.</summary>
    /// <value>The view gameobjects.</value>
    public List<GameObject> viewObjects
    {
        get
        {
            return views.Select((v) => v.gameObject).ToList();
        }
    }
    

    protected override void Awake()
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



    /// <summary>Goes to the gameobject's view if it is in this page's view list.</summary>
    /// <param name="viewObject">The view object to go to.</param>
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
    /// <summary>Goes to the given view.</summary>
    /// <param name="view">The view to go to.</param>
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

    //executed when this page is going to appear
    /// <summary>Executed when this page is going to appear, which is when it is navigated to.</summary>
    protected virtual void PageAppearing()
    {
        //tell current view it is about to appear if it exists
        if (currentView != null)
        {
            ViewAppearing(currentView);
        }
    }

    /// <summary>Executed when this page is going to disappear, which is when it is navigated away from.</summary>
    protected virtual void PageDisappearing()
    {
        //tell current view it is about to disappear if it exists
        if (currentView != null)
        {
            ViewDisappearing(currentView);
        }
    }

    //executed when a view in this page is going to appear or dissapear (navigating away or to view, or navigating away or to a page)

    /// <summary>Invoke the 'OnAppearing' function on a view component in the given gameobject if it is a child of this page.</summary>
    /// <param name="view">The gameobject who's view is about to appear.</param>
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

    /// <summary>Invokes the 'OnAppearing' function on in the given view if it is a child of this page.</summary>
    /// <param name="view">The view that is about to appear.</param>
    protected virtual void ViewAppearing(View view)
    {
        //check if view is in this page
        if (viewObjects.Contains(view.gameObject))
        {
            //run view specific code
            view.OnAppearing();
        }
    }
    /// <summary>Invoke the 'OnDisappearing' function on a view component in the given gameobject if it is a child of this page.</summary>
    /// <param name="view">The gameobject who's view is about to disappear.</param>
    protected virtual void ViewDisappearing(GameObject view)
    {
        //check if view is in this page
        if (viewObjects.Contains(view))
        {
            View viewComponent = view.GetComponent<View>();
            viewComponent.OnDisappearing();
        }
    }
    /// <summary>Invokes the 'OnDisappearing' function on in the given view if it is a child of this page.</summary>
    /// <param name="view">The view that is about to disappear.</param>
    protected virtual void ViewDisappearing(View view)
    {
        //check if view is in this page
        if (viewObjects.Contains(view.gameObject))
        {
            view.OnDisappearing();
        }
    }
}

}

