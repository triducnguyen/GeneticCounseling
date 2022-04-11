using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : StyleHandler
{  
    //page this view is within
    public PageController page;
    public Action OnAppearing { get => () => ViewAppearing(); }

    public Action OnDisappearing { get => () => ViewDisappearing(); }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void ViewAppearing()
    {

    }

    protected virtual void ViewDisappearing()
    {

    }
}
