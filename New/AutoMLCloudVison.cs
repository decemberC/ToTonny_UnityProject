using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using SimpleJSON;

public class AutoMLCloudVison : MonoBehaviour
{

    public string url ;
   
    public string scanningimg = "";
    private string accessCode = "";
    private WebCamTexture webcamTexture;
    public int maxResults = 10;
    public float wSecound;
    public string resultText;
    //public Renderer showCam;
    public static AutoMLCloudVison aiSearch;
    Texture2D texture2D;
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
        try{
        aiSearch = this;
        headers = new Dictionary<string, string>();

         webcamTexture = new WebCamTexture(WebCamTexture.devices[0].name, 720, 720);
        //showCam.material.mainTexture = webcamTexture;
        
        }catch(System.Exception err){
            Debug.Log(err);
        }
    }
 
    public IEnumerator Capture()
    {
        webcamTexture.Play();
        //Captrue Camera Data Begin
        Color[] pixels = webcamTexture.GetPixels();

        if (texture2D == null || webcamTexture.width != texture2D.width || webcamTexture.height != texture2D.height)
        {
            texture2D = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGBA32, false);
        }
        texture2D.SetPixels(pixels);

        byte[] jpg = texture2D.EncodeToJPG();
        string base64 = System.Convert.ToBase64String(jpg);
        Debug.Log(base64);
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
                    string fullText =res["predictions"][0]["class"].ToString();
              
                    Debug.Log("OCR Response: " +fullText);
                    if (fullText != "")
                    {
                       //DataManage.SearchClass = fullText;
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
            StopCoroutine(Capture());
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