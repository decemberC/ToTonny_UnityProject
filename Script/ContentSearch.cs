using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentSearch : MonoBehaviour
{
    
    public static DatabaseClass SearchItem(string feedback) {
        Debug.Log("test content");
        /*var result = from i in DataManager.DB
                        group i by i.KeyWord into x
                        where feedback.Contains(x.Key)
                        select new { x.Key};*/
        Debug.Log(feedback.Contains("OREO"));
        return null;

    } 
}
