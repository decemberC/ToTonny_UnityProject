using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageSettle : MonoBehaviour
{
    public string fieldName;
    private string nowFieldLan;
    public bool isTextMesh;
    private TextMeshProUGUI tm;
    private Text te;
    // Start is called before the first frame update
    void Start()
    {
        nowFieldLan = DataManage.appLan;
          if(isTextMesh){
                tm = GetComponent<TextMeshProUGUI>();
                tm.text = DataManage.languageTable[fieldName];
                tm.text = tm.text.Replace("\\n","\r\n");
               
            }else{
                te = GetComponent<Text>();
                te.text = DataManage.languageTable[fieldName];
                te.text = te.text.Replace("\\n","\r\n");
            }
    }

    // Update is called once per frame
    void Update()
    {
      
        if(nowFieldLan != DataManage.appLan){
            if(isTextMesh){
                
                tm.text = DataManage.languageTable[fieldName];
                tm.text = tm.text.Replace("\\n","\r\n");
               
            }else{
         
                te.text = DataManage.languageTable[fieldName];
                te.text = te.text.Replace("\\n","\r\n");
            }
            nowFieldLan = DataManage.appLan;
        }
    }
}
