using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SaveDataClass
{
    public string recordName;
    public string recordTime;
    public SaveDataClass(string strArr){
        recordName = strArr.Split('|')[0];
        recordTime = strArr.Split('|')[1];
    }
}
