using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ResourceLoader : MonoBehaviour
{
    AsyncOperation loader;
      void Start()
    {
        var JsonText = Resources.Load<TextAsset>("Json/json_database");
        var appSet = Resources.Load<TextAsset>("Json/AppSetting");
        //Debug.Log(JsonText); 
        //ReadDB Begin
        JObject jResult =  JObject.Parse(JsonText.text);
        loader = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
        loader.allowSceneActivation = false;
        foreach (var i in jResult["Content"].Children()){
         DataManager.DB.Add(i.ToObject<DatabaseClass>());
        }
        var dataInfo = typeof(DatabaseClass).GetFields();
        for(int c =0; c<dataInfo.Length;c++){
            DataManager.translate.Add(c,dataInfo[c].Name);
        }
        //ReadDB End
        //SerializeApp Begin
        SerilaizeJsonFormat appSetUp = JsonConvert.DeserializeObject<SerilaizeJsonFormat>(appSet.text);
        WakeUpManager.tagSetuper =new List<string>(appSetUp.tagArray);
        //SerializeApp End
    }

     
    

    void Update()
{
    if (loader.progress >= 0.9f)
    {
        loader.allowSceneActivation = true;
    }
}

private class SerilaizeJsonFormat{
    [JsonProperty("tag")]
    public string[] tagArray;
}
}


