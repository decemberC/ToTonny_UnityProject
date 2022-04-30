using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManage:MonoBehaviour
{
    public static string colorPicker;
    public static DataManage self;
    public static Dictionary<string,string> languageTable = new Dictionary<string, string>();
    public static string appLan ;
    public List<DatabaseClass> DB;
    void Start()
    {
        self=this;
        if(languageTable.Count == 0){
            
            appLan="En";
            SetLanguageTable(appLan);
        }
    }
    public static void SetLanguageTable(string language){
        if(languageTable.Count!=0){
            languageTable.Clear();
        }
        var loader = Resources.Load<TextAsset>("Language/"+language);
        
        string[] matchLanguage = loader.text.Split(new string[] {";\r\n"},System.StringSplitOptions.None);
        
     
        foreach(string i in matchLanguage){
            string[] ceil = i.Split(':');
            languageTable.Add(ceil[0],ceil[1]);
        }        
    }
}
