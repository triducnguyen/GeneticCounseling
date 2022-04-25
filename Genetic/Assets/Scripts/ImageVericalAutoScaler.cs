using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageVericalAutoScaler : MonoBehaviour
{
    public RectTransform imgTransform;
    public RawImage image;
    public LayoutElement element;
    public float ratio => image.mainTexture.width / image.mainTexture.height;
    public float width => imgTransform.sizeDelta.y / ratio;
    // Update is called once per frame
    void Update()
    {
        if (imgTransform.sizeDelta.x != width)
        {
            Debug.Log("Width: "+width);
            imgTransform.sizeDelta = new Vector2(width, imgTransform.sizeDelta.y);
            Debug.Log("New size: "+imgTransform.sizeDelta);
        }
    }

    public void SetSize(Vector2 size)
    {
        var ratio = image.mainTexture.width / image.mainTexture.height;
        imgTransform.sizeDelta = new Vector2(imgTransform.sizeDelta.x, (imgTransform.sizeDelta.x * ratio));
        
    }
    public static float map(float value, float leftMin, float leftMax, float rightMin, float rightMax)
    {
        return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
    }
}
