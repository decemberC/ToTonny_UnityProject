using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json.Linq;

public class ResourceLoader : MonoBehaviour
{
      void Start()
    {
        var JsonText = Resources.Load<TextAsset>("json_database");
        //Debug.Log(JsonText); 
        JObject jResult =  JObject.Parse(JsonText.text);
        foreach(var i in jResult["Content"].Children()){
         DataManager.DB.Add(i.ToObject<DatabaseClass>());
        }
        var dataInfo = typeof(DatabaseClass).GetFields();
        for(int c =0; c<dataInfo.Length;c++){
            DataManager.translate.Add(c,dataInfo[c].Name);
            
        }
    }

    /*void Update()
    {
        
    }*/
}
