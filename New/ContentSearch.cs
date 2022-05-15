using System.Collections;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentSearch : MonoBehaviour
{
    private static bool capped = false;
    public static bool _capped{
        get{
            return capped;
        }
    }
    public static DatabaseClass SearchItem(string feedback)
    {

        if (!capped)
        {
            capped = !capped;
            AutoMLCloudVison.aiSearch.StartScan();
            if (DataManage.searchClass != "")
            {
                var result = from i in DataManage.self.DB

                             where i.aiTag == DataManage.searchClass
                             select i;
                if (result.Count() > 0)
                {
                    return result.First();
                }
            }


        }
        else
        {

            if (DataManage.searchClass != "")
            {
                var result = from i in DataManage.self.DB

                             where i.aiTag == DataManage.searchClass
                             select i;
                if (result.Count() > 0)
                {
                    return result.First();
                }
            }

        }

        return null;
    }
    public static void ClearSearch()
    {
        capped = false;
        DataManage.searchClass = "";

    }

    /*private static async Task<float> PercentSearch(char[] compareChar, char[] benchmarkChar)
    {
        int outer = 0;
        List<char> addedChar = new List<char>(benchmarkChar);
        foreach (char b in benchmarkChar)
        {
            foreach (char c in compareChar)
            {
                if (await PercentSearch(c, b,addedChar))
                {
                    outer += 1;
                    addedChar.Remove(b);
                    continue;
                }
            }
        }
        Debug.Log(benchmarkChar.Length+"  "+ (float)outer / compareChar.Length*(benchmarkChar.Length-addedChar.Count));
        return (float)Math.Round((float)outer / compareChar.Length*(benchmarkChar.Length-addedChar.Count),4);
    }
    private static async Task<bool> PercentSearch(char compareChar, char benchmarkChar,List<char> checkList)
    {
        if (compareChar == benchmarkChar && checkList.Contains(compareChar))
        {
            return true;
        }
        else
            return false;


    }*/
}
