using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagSys : MonoBehaviour
{
    public static List<TagItem> TagList = new List<TagItem>();
    private static TagSys tagSys;
    public static TagSys _tagsys{
        get{
            return tagSys;
        }
    }
    public void Start(){
        tagSys=this;
    }
    public void SetTagItem(Toggle target){
        DeployTag(target.gameObject.GetComponent<TagItem>(),target.isOn);
    }
    /*DeployTag is for adding or deleteing tag in TagList. It will decide by the toggle setting*/
    private void DeployTag(TagItem tag, bool action){
        if(action){
        TagList.Add(tag);
        }else{
            TagList.Remove(tag);
        }
    }

}
