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
        JObject jResult = JObject.Parse(JsonText.text);
        foreach(var i in jResult["Content"].Children()){
        Debug.Log(i.ToObject<DatabaseClass>().Name);
        }
    }

    /*void Update()
    {
        
    }*/
}
