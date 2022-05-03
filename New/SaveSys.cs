using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSys : MonoBehaviour
{
    public void ReadHistory(){
        DataManage.historyList.Clear();
        string historyStr = PlayerPrefs.GetString("History");
        string[] historyNameNDate = historyStr.Split(';');
        foreach(var i in historyNameNDate){
            DataManage.historyList.Add(new SaveDataClass(i));
        }
    }
}
