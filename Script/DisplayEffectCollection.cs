using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayEffectCollection : MonoBehaviour
{
    public static void AlertTextChangRed(Text target){
        target.color = Color.red;
    }
    public static void AlertBGChangRed(Text target){
        target.GetComponentInParent<Image>().color = Color.red;
    }
}
