using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Newtonsoft.Json;
[CreateAssetMenu(menuName ="TagItem")]
public class TagItem:ScriptableObject
{
    //The members of ValueList should follow
    //the fields of DatabaseClass 
   public enum ValueList{
    Name,
     KeyWord,
     Energy,
     Protein,
     Saturated,
     TransFat,
     Carbohydrate,
     Sugars,
     Sodium,
     Gluten,
     Shellfish,
     Allergy
   }
   public enum TagType{
     Display,
     Alert
   }
    public ValueList value = new ValueList();
    public TagType type = new TagType();
    public bool causeAlert;
    public bool displayable;
}
