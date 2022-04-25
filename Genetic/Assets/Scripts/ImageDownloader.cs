using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageDownloader : MonoBehaviour
{
    public void GetImage(string url, RawImage image)
    {
        var finalUrl = getFinalUrl(url);
        StartCoroutine(SetImage(finalUrl, image));
    }

    IEnumerator SetImage(string url, RawImage image)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            Debug.Log("URL: "+url);
            image.gameObject.SetActive(false);
        }
        else
        {
            image.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            var ratio = image.mainTexture.width / image.mainTexture.height;
            var newSize = image.GetComponent<RectTransform>();
            //var imgAutoScaler = image.GetComponent<ImageVericalAutoScaler>();
            //imgAutoScaler.SetSize(new Vector2(image.mainTexture.width, (newSize.sizeDelta.x * ratio)));
            //var layout = image.GetComponent<LayoutElement>();
            //layout.preferredHeight = newSize.sizeDelta.x;
            Debug.Log("Set image texture from web");
            image.gameObject.SetActive(true);
        }
    }

    public static string getFinalUrl(string url)
    {
        //check content type
        var client = new HttpClient();
        var response = client.GetAsync(url).Result;
        if (response.Headers != null)
        {
            if (response.Content.Headers.ContentType.MediaType.StartsWith("image/"))
            {
                //url is an image, use raw url
                return url;
            }
        }
        string id = getIdFromUrl(url);
        return "https://drive.google.com/uc?id=" + id;

    }

    public static string getIdFromUrl(string url)
    {
        Regex r = new Regex(@"\/d\/(.+)\/", RegexOptions.IgnoreCase);
        Match m = r.Match(url);
        return m.ToString().TrimStart('/', 'd').Trim('/');
    }
}
