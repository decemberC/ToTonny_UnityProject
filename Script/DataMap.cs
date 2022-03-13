using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataMap : MonoBehaviour
{
    public Text[] dataPlan;
    public void MapDate()
    {
        DatabaseClass searchResultDup = DataManager.searchResult;
        Dictionary<int, string> dicDup = DataManager.translate;
 
        if (searchResultDup == null)
        {
            Debug.Log("null");
            return;
        }
        for (int i = 0; i < dicDup.Count; i++)
        {
            dataPlan[i].text = typeof(DatabaseClass).GetField(dicDup[i]).GetValue(searchResultDup).ToString();;
        }
    }
}
