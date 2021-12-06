using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Swipe : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public NavigationController navigation { get => NavigationController.instance; }
    private float minDistanceSwipe = 0.2f;
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag Begin");
        //throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");
        //throw new System.NotImplementedException();
    }
}
