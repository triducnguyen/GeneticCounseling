using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Swipe : MonoBehaviour, IDragHandler, IEndDragHandler
{

    public FlyoutController menu;
    public NavbarButtonController menuButton;
    private float minDistanceSwipe = 0.2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float percent = (eventData.pressPosition.x - eventData.position.x) / Screen.width;

        if (Mathf.Abs(percent) > minDistanceSwipe)
        {
            if (percent < 0) /*Swipe left*/ 
            {
                menu.FlyoutTapped(false);
                menuButton.Swipped();

            } else if (percent > 0) /*Swipe right*/
            {
                menu.FlyoutTapped(true);
                menuButton.Swipped();

            }
        }
    }


    public void OnDrag(PointerEventData eventData)
    {

    }
}
