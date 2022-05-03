using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HistoryPanel : Panel
{


    public GameObject GeneratePrefab()
    {
        GameObject instance = GameObject.Instantiate(subPanelprefab, scrollAddPos.GetComponent<RectTransform>());
        instance.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, distance);
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        //historyList= new List<GameObject>();
        if (DataManage.historyList.Count > 0)
        {
            foreach (var i in DataManage.historyList)
            {
               GameObject mapper =  GeneratePrefab();
               DatabaseClass searchR =( from d in  DataManage.self.DB
                                       where d.dName == i.recordName
                                       select d).First();
               if(searchR.gluten){
                   mapper.GetComponent<RectTransform>().Find("gcolor").gameObject.SetActive(true);
               }
               if(searchR.shellfish){
                   mapper.GetComponent<RectTransform>().Find("scolor").gameObject.SetActive(true);
               }
               if(!searchR.gluten&&!searchR.shellfish){
                   mapper.GetComponent<RectTransform>().Find("icolor").gameObject.SetActive(true);
               }
               mapper.GetComponent<RectTransform>().Find("Name").gameObject.GetComponent<Text>().text = i.recordName;
               mapper.GetComponent<RectTransform>().Find("Name").gameObject.GetComponent<LanguageSettle>().fieldName =i.recordName+"_ForHistory";
               mapper.GetComponent<RectTransform>().Find("Time").gameObject.GetComponent<Text>().text = i.recordTime;
            }
        }
        else
        {
            NoData();
        }
    }

}
