using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>Automatically sizes an image vertically based on its given width, using the ratio of the original image.</summary>
public class ImageVericalAutoScaler : MonoBehaviour
{
    /// <summary>The image transform.</summary>
    public RectTransform imgTransform;
    /// <summary>The image
    /// to resize.</summary>
    public RawImage image;
    /// <summary>Gets the ratio.</summary>
    /// <value>The ratio of the image to resize.</value>
    public float ratio => image.mainTexture.width / image.mainTexture.height;
    /// <summary>
    /// Gets the width using the ratio.
    /// </summary>
    /// <value>The width.</value>
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

    /// <summary>Sets the size of the image transform.</summary>
    /// <param name="size">The size.</param>
    public void SetSize(Vector2 size)
    {
        var ratio = image.mainTexture.width / image.mainTexture.height;
        imgTransform.sizeDelta = new Vector2(imgTransform.sizeDelta.x, (imgTransform.sizeDelta.x * ratio));
        
    }
}
