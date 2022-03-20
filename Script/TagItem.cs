using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagItem:MonoBehaviour
{
    public string value ;
    public bool causeAlert;
    public bool displayable;
    public void Start(){
        Toggle toggle=GetComponent<Toggle>();
        if(WakeUpManager.tagSetuper.Contains(gameObject.name)){
        toggle.isOn=true;
        TagSys._tagsys.SetTagItem(toggle);
    }else{
        toggle.isOn=false;
    }
    }
}
