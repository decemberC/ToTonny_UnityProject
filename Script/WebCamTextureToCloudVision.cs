using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using SimpleJSON;

public class WebCamTextureToCloudVision : MonoBehaviour
{

    public string url = "https://vision.googleapis.com/v1/images:annotate?key=";
    public string apiKey = "";
    public float capturePerTime = 5.0f;
    public int requestedWidth = 640;
    public int requestedHeight = 480;
    public FeatureType featureType = FeatureType.TEXT_DETECTION;
    public int maxResults = 10;
    public GameObject ARPanel;
    public Text resultText, resultArray;
    public WebCamTexture webcamTexture;
    public static WebCamTextureToCloudVision aiSearch;
    Texture2D texture2D;
    Dictionary<string, string> headers;

    [System.Serializable]
    public class AnnotateImageRequests
    {
        public List<AnnotateImageRequest> requests;
    }

    [System.Serializable]
    public class AnnotateImageRequest
    {
        public ImageContext imageContext; 
        public Image image;
        public List<Feature> features;
    }

    [System.Serializable]
    public class Image
    {
        public string content;
    }

    [System.Serializable]
    public class ImageContext {
        public string[] languageHints;
    }
    [System.Serializable]
    public class Feature
    {
        public string type;
        public int maxResults;
    }

    public enum FeatureType
    {
        TYPE_UNSPECIFIED,
        FACE_DETECTION,
        LANDMARK_DETECTION,
        LOGO_DETECTION,
        LABEL_DETECTION,
        TEXT_DETECTION,
        SAFE_SEARCH_DETECTION,
        IMAGE_PROPERTIES
    }

    void Start()
    {
        aiSearch=this;
        headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json; charset=UTF-8");
        if (apiKey == null || apiKey == "")
            Debug.LogError("No API key. Please set your API key into the \"Web Cam Texture To Cloud Vision(Script)\" component.");

        WebCamDevice[] devices = WebCamTexture.devices;
        for (var i = 0; i < devices.Length; i++)
        {
            Debug.Log(devices[i].name);
        }
        if (devices.Length > 0)
        {
            webcamTexture = new WebCamTexture(devices[0].name, requestedWidth, requestedHeight);
            Renderer r = GetComponent<Renderer>();
            if (r != null)
            {
                Material m = r.material;
                if (m != null)
                {
                    m.mainTexture = webcamTexture;
                }
            }
            else
            {
                Debug.Log("No Renderer");
            }
            webcamTexture.Play();
            ARPanel.GetComponent<RawImage>().texture = webcamTexture;
            StartCoroutine("Capture");
        }
    }
  public void RebootCapture(){
        StartCoroutine("Capture");
    }

    // Update is called once per frame
    

    private IEnumerator Capture()
    {
        while (true)
        {
            if (this.apiKey == null)
                yield return null;

            yield return new WaitForSeconds(capturePerTime);
            webcamTexture.Play();
            Color[] pixels = webcamTexture.GetPixels();
            if (pixels.Length == 0)
                yield return null;
            if (texture2D == null || webcamTexture.width != texture2D.width || webcamTexture.height != texture2D.height)
            {
                texture2D = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGBA32, false);
            }
            texture2D.SetPixels(pixels);

            byte[] jpg = texture2D.EncodeToJPG();
            string base64 = System.Convert.ToBase64String(jpg);
            Debug.Log(base64);

            AnnotateImageRequests requests = new AnnotateImageRequests();
            requests.requests = new List<AnnotateImageRequest>();

            AnnotateImageRequest request = new AnnotateImageRequest();
            request.image = new Image();
            request.imageContext = new ImageContext();
            request.imageContext.languageHints = new string[] {
                "en" , "zh-Hant"
            };
            request.image.content = base64;
            request.features = new List<Feature>();
            Feature feature = new Feature();
            feature.type = this.featureType.ToString();
            feature.maxResults = this.maxResults;
            request.features.Add(feature);
            requests.requests.Add(request);

            string jsonData = JsonUtility.ToJson(requests, false);
            if (jsonData != string.Empty)
            {
                string url = this.url + this.apiKey;
                byte[] postData = System.Text.Encoding.Default.GetBytes(jsonData);
                using (WWW www = new WWW(url, postData, headers))
                {
                    yield return www;
                    if (string.IsNullOrEmpty(www.error))
                    {
                        string responses = www.text.Replace("\n", "").Replace(" ", "");
                        JSONNode res = JSON.Parse(responses);
                        string fullText = res["responses"][0]["textAnnotations"][0]["description"].ToString().Trim('"');
                        if (fullText != "")
                        {
                            Debug.Log("OCR Response: " + fullText);
                            ARPanel.SetActive(true);
                            resultText.text = fullText.Replace("\\n", " ");
                            fullText = fullText.Replace("\\n", ";");
                            string[] texts = fullText.Split(';');
                            resultArray.text = "";
                            for (int i = 0; i < texts.Length; i++)
                            {
                                resultArray.text += texts[i];
                                if (i != texts.Length - 1)
                                    resultArray.text += ", ";
                            }
                            DataManager.searchResult = ContentSearch.SearchItem(resultArray.text);
                            if (DataManager.searchResult != null)
                            {
                                
                                ShowData.DisplayData();
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Error: " + www.error);
                    }
                }
            }
        }
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