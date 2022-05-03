using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.TestTools;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.Rendering;
public class TrackImageTest : MonoBehaviour
{
    public Camera arCam;

    private DatabaseClass searchResult;
    public Image image;
    public ARTrackedImageManager imanager;
    public Light ilight;
    public Text result;
    // Start is called before the first frame update
    void OnEnable()
    {
        imanager.trackedImagePrefab = new GameObject("Emptypref");
        imanager.trackedImagesChanged += ImageReturn;

    }
    void OnDisable()
    {
        imanager.trackedImagesChanged -= ImageReturn;

    }
    public void ImageReturn(ARTrackedImagesChangedEventArgs args)
    {
        arCam.transform.rotation =Quaternion.identity;
       
        foreach (var item in args.updated)
        {
            if (item.trackingState == TrackingState.Tracking)
            {
                string serachName = item.referenceImage.name.Remove(item.referenceImage.name.IndexOf('_'));
                //WebCamTextureToCloudVision.aiSearch.ActivateCapture(arCam, item.referenceImage.name);
               
                    searchResult = ContentSearch.SearchItem(serachName);
                    PlayerPrefs.SetString("History",PlayerPrefs.GetString("History")+searchResult.dName+'|'+System.DateTime.Now.ToString("dd/MM/yyyy HH:mm")+';');
                    ShowPrefabControl.self.ShowPrefabByData(searchResult, item, arCam);
                    
                /*if (searchResult != null)
                {
                    ShowPrefabControl.self.ShowPrefabByData(searchResult, item);
                }*/

                //result.text = item.referenceImage.name;

            }
        }
        if (args.updated.Count != 0)
        {
            foreach (var delItem in imanager.trackables)
            {
                if (delItem.trackingState != TrackingState.Tracking)
                {
                    string serachName = delItem.referenceImage.name.Remove(delItem.referenceImage.name.IndexOf('_'));
                    if (args.added.Contains(delItem))
                        args.added.Remove(delItem);
                    if (args.updated.Contains(delItem))
                        args.updated.Remove(delItem);
                    WebCamTextureToCloudVision.aiSearch.ReleaseCapture(delItem.referenceImage.name);
                    ShowPrefabControl.self.NoShowPrefab(delItem);
                    
                    
                }
            }
        }
        //result.text = args.updated.Count.ToString();
    }

}
