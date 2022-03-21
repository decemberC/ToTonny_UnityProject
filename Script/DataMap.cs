using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        ShowData._showData.showPanel.SetActive(true);
        string fieldVaule ="";
        for (int i = 0; i < dicDup.Count; i++)
        {
            var findTag =from r in TagSys.TagList
                         where dicDup[i] == r.value.ToString()
                         select r;
            try{
            TagItem tagResult =findTag.First();
            fieldVaule = typeof(DatabaseClass).GetField(dicDup[i]).GetValue(searchResultDup).ToString();
            if(tagResult.displayable){
            dataPlan[i].text = fieldVaule;}else{
                dataPlan[i].gameObject.SetActive(false);
            }
            if(tagResult.causeAlert)
            if(searchResultDup.Gluten&&dicDup[i] =="Gluten"){
            DisplayEffectCollection.AlertTextChangRed(dataPlan[i]);}
            if(searchResultDup.Shellfish&&dicDup[i] =="Shellfish"){
            DisplayEffectCollection.AlertTextChangRed(dataPlan[i]);}}
            catch(System.Exception err){
                Debug.Log("FieldName"+dicDup[i]);
                Debug.Log(TagSys.TagList.Count());
            }
        }
    }

}
