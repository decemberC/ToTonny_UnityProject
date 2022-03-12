using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentSearch : MonoBehaviour
{

    public static DatabaseClass SearchItem(string feedback)
    {
        Debug.Log("test content");
        var result = from i in DataManager.DB
                     group i by i.KeyWord into x
                     where feedback.Contains(x.Key)
                     select x;
        
        foreach (var searchRes in result)
        {
            foreach (var data in searchRes)
            {
                
                if (feedback.Contains(data.Name))
                {
                    return data;
                }
            }
        }
        return null;

    }
}
