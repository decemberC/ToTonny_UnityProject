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
                     group new
                     {
                         i.Allergy,
                         i.Carbohydrate,
                         i.Energy
                     ,
                         i.Gluten,
                         i.Name,
                         i.Protein,
                         i.Saturated,
                         i.Shellfish
                     ,
                         i.Sodium,
                         i.Sugars,
                         i.TransFat
                     } by i.KeyWord into x
                     where feedback.Contains(x.Key)
                     select x;
        Debug.Log(result.Count());
        foreach (var searchRes in result)
        {Debug.Log(searchRes.Count());
            foreach (var data in searchRes)
            {
                if (feedback.Contains(data.Name))
                {
                    Debug.Log(data.Name);
                }
            }
        }
        return null;

    }
}
