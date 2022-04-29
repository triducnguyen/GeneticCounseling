using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace App.Utility
{
/// <summary>A script for downloading images from google drive share links or direct png links.</summary>
    public class ImageDownloader : MonoBehaviour
    {
        /// <summary>Gets an image from a url and loads it into the RawImage.
        /// Valid URLs can be of google drive share links and direct png links.</summary>
        /// <param name="url">The image URL.</param>
        /// <param name="image">The image component to display the URL image.</param>
        public void GetImage(string url, RawImage image)
        {
            var finalUrl = getFinalUrl(url);
            StartCoroutine(SetImage(finalUrl, image));
        }

        /// <summary>Sets the RawImage component's texture to be the image loaded from the provided URL.</summary>
        /// <param name="url">The image URL to load.</param>
        /// <param name="image">The RawImage component to display the URL image.</param>
        /// <returns>Returns when complete.</returns>
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

        /// <summary>Gets the final URL after resolving to the proper URL.</summary>
        /// <param name="url">The URL to resolve.</param>
        /// <returns>Resolved URL. Unchanged if the URL already points to a raw image.</returns>
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
            //create final url from image id
            return "https://drive.google.com/uc?id=" + id;

        }

        /// <summary>Gets the image identifier from google drive share URL.</summary>
        /// <param name="url">The URL.</param>
        /// <returns>An image ID of a shared google drive image.</returns>
        public static string getIdFromUrl(string url)
        {
            Regex r = new Regex(@"\/d\/(.+)\/", RegexOptions.IgnoreCase);
            Match m = r.Match(url);
            return m.ToString().TrimStart('/', 'd').Trim('/');
        }
    }
}


