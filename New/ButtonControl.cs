using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl:MonoBehaviour{
    public void ChangeLanguage(string lan){
        DataManage.appLan = lan;
        DataManage.SetLanguageTable(lan);
    }
}
