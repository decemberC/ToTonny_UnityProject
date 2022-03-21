using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentSearch : MonoBehaviour
{

    public static async Task<DatabaseClass> SearchItem(string feedback)
    {
    
        var result = from i in DataManager.DB
                     group i by i.KeyWord into x
                     where feedback.Contains(x.Key)
                     select x;

        foreach (var searchRes in result)
        {
            foreach (var data in searchRes)
            {
                if (await PercentSearch(feedback.ToCharArray(), data.Name.ToArray()) >= 0.7f)
                {
                    return data;
                }
                /*if (feedback.Contains(data.Name))
                {
                    return data;
                }*/
            }
        }
        return null;

    }
    private static async Task<float> PercentSearch(char[] compareChar, char[] benchmarkChar)
    {
        int outer = 0;
        List<char> addedChar = new List<char>(benchmarkChar);
        foreach (char b in benchmarkChar)
        {
            foreach (char c in compareChar)
            {
                Debug.Log(c+"    "+addedChar.Count);
                if (await PercentSearch(c, b,addedChar))
                {
                    outer += 1;
                    addedChar.Remove(b);
                    continue;
                }
            }
        }

        return (float)outer / benchmarkChar.Length;
    }
    private static async Task<bool> PercentSearch(char compareChar, char benchmarkChar,List<char> checkList)
    {
        if (compareChar == benchmarkChar && checkList.Contains(compareChar))
        {
            return true;
        }
        else
            return false;


    }
}
