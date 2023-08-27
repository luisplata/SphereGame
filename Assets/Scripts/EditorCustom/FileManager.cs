using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FileManager : MonoBehaviour, IFileManager
{
    [SerializeField] private TextMeshProUGUI result;
    public void Config(IRulesMediator allRules)
    { 
        
    }

    public string CreateJson(List<DragComponent> listOfDrags)
    {
        var result = "";
        var listOfElements = new List<string>();
        foreach (var dragComponent in listOfDrags)
        {
            listOfElements.Add(dragComponent.GetJson());
        }
        //Example of result: {"elements":[{"Element":"PointToStart","PositionX":-2,"PositionY":3,"Layer":0,"Rotation":-90,"Data":""}]}
        result = "{\"elements\":[" + string.Join(",", listOfElements) + "]}";
        Debug.Log(result);
        return result;
    }
}