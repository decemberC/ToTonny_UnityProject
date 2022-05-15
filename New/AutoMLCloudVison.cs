using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using SimpleJSON;

public class AutoMLCloudVison : MonoBehaviour
{

    public string url;

    public string scanningimg = "";
    private string accessCode = "";
    public Camera capCam;
    public int maxResults = 10;
    public float wSecound;
    public string resultText;
    public Renderer render;
    public static AutoMLCloudVison aiSearch;
    Texture2D texture2D, cropedTexture;
    Dictionary<string, string> headers;


    [System.Serializable]
    public class AutoMLRequests
    {
        public List<AutoMLRequest> requests;
    }

    [System.Serializable]
    public class AutoMLRequest
    {
        public Payload payload;

    }

    [System.Serializable]
    public class Payload
    {
        public Image image;

    }

    [System.Serializable]
    public class Image
    {
        public string imageBytes;
    }


    [System.Serializable]
    public class pParams
    {
        public float scoreThreshold;
        public int maxBoundingBoxCount;
    }



    void Start()
    {
        try
        {
            aiSearch = this;
            headers = new Dictionary<string, string>();
            capCam.targetTexture.width = Display.main.systemWidth;
            capCam.targetTexture.height = Display.main.systemHeight;
            cropedTexture = new Texture2D((int)(capCam.targetTexture.width/1.4f),(int)(capCam.targetTexture.height/1.2f));
            //showCam.material.mainTexture = webcamTexture;

        }
        catch (System.Exception err)
        {
            Debug.Log(err);
        }
    }

    public void StartScan()
    {
        StartCoroutine(Capture());
    }
    public IEnumerator Capture()
    {
       
yield return new WaitForEndOfFrame();
        capCam.Render();

        //Captrue Camera Data Begin


        if (texture2D == null || capCam.targetTexture.width != texture2D.width || capCam.targetTexture.width != texture2D.height)
        {
            texture2D = new Texture2D(capCam.targetTexture.width, capCam.targetTexture.height, TextureFormat.RGBA32, false);
        }
        texture2D.ReadPixels(new Rect(0, 0, capCam.targetTexture.width, capCam.targetTexture.height), 0, 0);
        texture2D.Apply();
        Color[] cropper = texture2D.GetPixels(texture2D.width-(int)(texture2D.width/1.4f),  texture2D.height-(int)(texture2D.height/1.2f),(int)(texture2D.width/1.4f), (int)(texture2D.height/1.2f));
        
        cropedTexture.SetPixels(cropper);
        cropedTexture.Apply();

        // render.material.mainTexture = texture2D;
        byte[] jpg = cropedTexture.EncodeToJPG();
        //File.WriteAllBytes("/storage/emulated/0/DCIM/" + System.DateTime.UtcNow.ToString("HH_mm_dd_MM_yyyy") + ".jpg", jpg);
        string base64 = System.Convert.ToBase64String(jpg);
        //Debug.Log(base64);
        //Captrue Camera Data End

        //Create Request send to Google Begin
        /*AutoMLRequests arequests = new AutoMLRequests();
        arequests.requests = new List<AutoMLRequest>();

        AutoMLRequest request = new AutoMLRequest();
        request.payload = new Payload();
        request.payload.image = new Image();
        request.payload.image.imageBytes = base64;
        arequests.requests.Add(request);
        string jsonData = JsonUtility.ToJson(request, false);*/
        //Create Request send to Google End
        //Debug.Log(jsonData);


        byte[] postData = System.Text.Encoding.Default.GetBytes(base64);


        using (WWW www = new WWW(url, postData, headers))
        {
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                string responses = www.text.Replace("\n", "").Replace(" ", "");
                JSONNode res = JSON.Parse(responses);
                string fullText =res["predictions"][0]["class"].ToString().Replace("\"", "");

                Debug.Log("OCR Response: " +fullText);
                if (fullText != "" && ContentSearch._capped)
                {
                   DataManage.searchClass = fullText;
                }
                else
                {
                    Debug.Log("Emypt but no error");
                }
            }
            else
            {
                Debug.Log("Error: " + www.error);
            }
        }
         
        //yield return null;
        StopAllCoroutines();
    }

#if UNITY_WEBGL
    void OnSuccessFromBrowser(string jsonString) {
        Debug.Log(jsonString);  
    }

    void OnErrorFromBrowser(string jsonString) {
        Debug.Log(jsonString);  
    }
#endif

}