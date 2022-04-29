using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>A visual element within a page.</summary>
public class View : StyleHandler
{
    //page this view is within
    /// <summary>The page
    /// this view belongs to.</summary>
    public PageController page;
    /// <summary>The action to perform when the view is appearing.</summary>
    /// <value>The action to perform.</value>
    public Action OnAppearing { get => () => ViewAppearing(); }
    /// <summary>The action to perform when the view is disappearing.</summary>
    /// <value>The action to perform.</value>
    public Action OnDisappearing { get => () => ViewDisappearing(); }


    /// <summary>Called by default when the view appears.</summary>
    protected virtual void ViewAppearing()
    {

    }
    /// <summary>Called by default when the view disappears.</summary>
    protected virtual void ViewDisappearing()
    {

    }
}
