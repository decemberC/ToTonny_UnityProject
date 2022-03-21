using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TagButton : MonoBehaviour
{
    public TagItem relateTag;
    Toggle clicker;
    // Start is called before the first frame update
    void Start()
    {
        clicker = GetComponent<Toggle>();
        switch( relateTag.type){
            case TagItem.TagType.Display:
            clicker.isOn=relateTag.displayable;
            break;
            case TagItem.TagType.Alert:
            clicker.isOn=relateTag.causeAlert;
            break;
        }
    }
}
