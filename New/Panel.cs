using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    public GameObject negativePanel;
    public GameObject subPanelprefab;
    public GameObject scrollAddPos;
    public float distance;
    public ScrollRect scrollerObject;
    

    public void NoData(){
        negativePanel.SetActive(true);
        scrollerObject.enabled=false;
    } 
}
