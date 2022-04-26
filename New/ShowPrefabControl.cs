using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ShowPrefabControl : MonoBehaviour
{
    private Dictionary<string, GameObject> showList = new Dictionary<string, GameObject>();
    public List<GameObject> prefabList;
    public static ShowPrefabControl self;
    private string showingprefab;
    void Start()
    {
        self = this;
        showingprefab ="";
    }
    // Start is called before the first frame update
    public void ShowPrefabByData(DatabaseClass data, ARTrackedImage item , Camera posCamera )
    {
        if(showingprefab == ""||item.referenceImage.name ==showingprefab){
            showingprefab =item.referenceImage.name;
            int showedNum = 0; 
            //item.transform.SetParent(posCamera.transform.parent);
           
        foreach (GameObject i in prefabList)
        {
            string pName = item.referenceImage.name + "_" + i.name;
            if (i.name == "Gluten" && data.gluten)
            {
                
                if (!showList.ContainsKey(pName))
                {
                    GameObject instaGameobj = GameObject.Instantiate(i, new Vector3(0+showedNum*0.02f , 0,0), Quaternion.identity);
                    showedNum++;
                    // instaGameobj.transform.localScale =new Vector3(0.02f,0.02f,0.02f); 
                    showList.Add(pName, instaGameobj);
                }
                else if (!(showList[pName] == null) && !showList[pName].activeInHierarchy)
                {
                    showList[pName].transform.position = new Vector3(item.transform.position.x+showedNum*0.02f, item.transform.position.y, item.transform.position.z);
                    //showList[pName].transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                    showedNum++;
                    showList[pName].SetActive(true);
                }
                else
                {
                    showList[pName].transform.rotation = item.transform.rotation;
                    showList[pName].transform.position = new Vector3(item.transform.position.x+showedNum*0.02f, item.transform.position.y, item.transform.position.z);
                    
                    showedNum++;
                    //showList[pName].transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                }
                
                continue;
            }
            if (i.name == "Shellfish" && data.shellfish)
            {


                if (!showList.ContainsKey(pName))
                {
                    GameObject instaGameobj = GameObject.Instantiate(i, new Vector3(0+showedNum*0.02f , 0,0), Quaternion.identity,item.transform);
                    showedNum++;
                    //instaGameobj.transform.localScale =new Vector3(0.02f,0.02f,0.02f); 
                    Debug.Log(instaGameobj.transform.lossyScale);
                    showList.Add(pName, instaGameobj);
                }
                else if (!(showList[pName] == null) && !showList[pName].activeInHierarchy)
                {
                    showList[pName].transform.position = new Vector3(item.transform.position.x+showedNum*0.02f, item.transform.position.y, item.transform.position.z);
                    //showList[pName].transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                    
                    showedNum++;
                    Debug.Log(showList[pName].transform.position);
                    showList[pName].SetActive(true);

                }
                else
                {
                    showList[pName].transform.rotation = item.transform.rotation;
                    showList[pName].transform.position = new Vector3(item.transform.position.x+showedNum*0.02f, item.transform.position.y, item.transform.position.z);
                    
                    //showList[pName].transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                    showedNum++;
                    Debug.Log(showList[pName].transform.position);
                }
                continue;
            }

        }}
    }
    public void NoShowPrefab(ARTrackedImage item)
    {
        foreach (GameObject i in prefabList)
        {
            string pName = item.referenceImage.name + "_" + i.name;
            if (showList.ContainsKey(pName))
            {
                showList[pName].SetActive(false);
            }
            if(item.referenceImage.name == showingprefab){
                showingprefab ="";
            }
        }

    }
}
