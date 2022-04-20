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
    public void ShowPrefabByData(DatabaseClass data, ARTrackedImage item)
    {
        if(showingprefab == ""||item.referenceImage.name ==showingprefab){
            showingprefab =item.referenceImage.name; 
        foreach (GameObject i in prefabList)
        {
            string pName = item.referenceImage.name + "_" + i.name;
            if (i.name == "Gluten" && data.gluten)
            {

                if (!showList.ContainsKey(pName))
                {
                    GameObject instaGameobj = GameObject.Instantiate(i, new Vector3(0 + showList.Count * 0.02f, 0, 0), Quaternion.identity);
                    instaGameobj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    showList.Add(pName, instaGameobj);
                }
                else if (!(showList[pName] == null) && !showList[pName].activeInHierarchy)
                {

                    showList[pName].transform.position = new Vector3(item.transform.position.x + prefabList.IndexOf(i) * 0.02f, item.transform.position.y, item.transform.position.z);
                    showList[pName].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    showList[pName].SetActive(true);
                }
                else
                {
                    showList[pName].transform.position = new Vector3(item.transform.position.x + prefabList.IndexOf(i) * 0.02f, item.transform.position.y, item.transform.position.z);
                    showList[pName].transform.rotation = item.transform.rotation;
                    showList[pName].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                }
                continue;
            }
            if (i.name == "Shellfish" && data.shellfish)
            {


                if (!showList.ContainsKey(pName))
                {
                    GameObject instaGameobj = GameObject.Instantiate(i, new Vector3(0 + showList.Count * 0.02f, 0, 0), Quaternion.identity);
                    instaGameobj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                    showList.Add(pName, instaGameobj);
                }
                else if (!(showList[pName] == null) && !showList[pName].activeInHierarchy)
                {

                    showList[pName].transform.position = new Vector3(item.transform.position.x + prefabList.IndexOf(i) * 0.02f, item.transform.position.y, item.transform.position.z);
                    showList[pName].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    showList[pName].SetActive(true);

                }
                else
                {
                    showList[pName].transform.position = new Vector3(item.transform.position.x + prefabList.IndexOf(i) * 0.02f, item.transform.position.y, item.transform.position.z);
                    showList[pName].transform.rotation = item.transform.rotation;
                    showList[pName].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
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
