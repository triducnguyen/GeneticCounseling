using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>Detects a simple swipe.</summary>
public class Swipe : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    /// <summary>Gets the navigation controller.</summary>
    /// <value>The navigation controller.</value>
    public NavigationController navigation { get => NavigationController.instance; }
    private float minDistanceSwipe = 0.2f;
    /// <summary>Called when dragging has ended.</summary>
    /// <param name="eventData">The event data.</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag ended");
        float percent = (eventData.pressPosition.x - eventData.position.x) / Screen.width;

        if (Mathf.Abs(percent) > minDistanceSwipe)
        {
            if (percent > 0) /*Swipe left*/
            {
                Debug.Log("Swiped left");
                navigation.CloseFlyout();

            }
            else if (percent < 0) /*Swipe right*/
            {
                Debug.Log("Swipe Right");
                navigation.OpenFlyout();
            }
        }
    }

    /// <summary>Called when dragging begins.</summary>
    /// <param name="eventData">The pointer event data.</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag Begin");
        //throw new System.NotImplementedException();
    }

    /// <summary>Called when dragging.</summary>
    /// <param name="eventData">The pointer event data.</param>
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");
        //throw new System.NotImplementedException();
    }
}
