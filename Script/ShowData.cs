using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowData : MonoBehaviour
{
   public bool isShowOnSamePage;//if this option is true, the data will show on same page. 
                                //Otherwise the data will show on other page
    public string changeSceneName;//Input the scene name which will redirect to show data.

    public GameObject showPanel;//The panel will display the data

    private static ShowData showData;
    public static ShowData _showData{
        get{
            return showData!=null?showData:null;
        }
        
    }
    public void Start()
    {
        if(showData == null){
            showData = this;
        }
    }
 public void ClosePannel(){
        showData.showPanel.SetActive(false);
    }
    public static void DisplayData(){
        if(showData.isShowOnSamePage){
            DinS();
        }else{
            DinO();
        }
    }
    private static void DinS(){
        WebCamTextureToCloudVision.aiSearch.StopAllCoroutines();
        showData.showPanel.GetComponent<DataMap>().MapDate();
    }
    private static void DinO(){}
}
