using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ResourceLoader : MonoBehaviour
{
    public List<TagItem> LoadTagItem;
    AsyncOperation loader;
      void Start()
    {
        var JsonText = Resources.Load<TextAsset>("Json/json_database");
        //Debug.Log(JsonText); 
        //ReadDB Begin
        JObject jResult =  JObject.Parse(JsonText.text);
        loader =  SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
        loader.allowSceneActivation = false;
        foreach (var i in jResult["Content"].Children()){
         DataManager.DB.Add(i.ToObject<DatabaseClass>());
        }
        var dataInfo = typeof(DatabaseClass).GetFields();
        for(int c =0; c<dataInfo.Length;c++){
            DataManager.translate.Add(c,dataInfo[c].Name);
        }
        //ReadDB End
        TagSys.TagList=LoadTagItem;
    }

     
    

    void Update()
{
    if (loader.progress >= 0.9f)
    {
        loader.allowSceneActivation = true;
    }
}

}


