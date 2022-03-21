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
    
    /*DeployTag is for adding or deleteing tag in TagList. It will decide by the toggle setting*/
    public void DeployTag(TagItem tag){
        switch(tag.type){
            case TagItem.TagType.Display:
            tag.displayable = !tag.displayable;
            break;
            
            case TagItem.TagType.Alert:
            tag.causeAlert = !tag.causeAlert;
            break;
        }
    }

}
