using UnityEngine;

public class DragComponent : MonoBehaviour
{
    private string _nameOfElement;
    private BaseElementInScene _elementInScene;
    public DragComponent ConfigureDragComponent(BaseElementInScene elementInScene, string nameOfElement)
    {
        //instantiate element and take to son to this eleent
        var element = Instantiate(elementInScene, transform);
        element.transform.localPosition = Vector3.zero;
        _nameOfElement = nameOfElement;
        _elementInScene = element;
        return this;
    }

    public string GetJson()
    {
        // example
        //{"Element":"PointToStart","PositionX":-2,"PositionY":3,"Layer":0,"Rotation":-90,"Data":""}
        var result = "{\"Element\":\"" + _nameOfElement + "\",\"PositionX\":" + _elementInScene.transform.position.x + ",\"PositionY\":" + _elementInScene.transform.position.y + ",\"Layer\":" + _elementInScene.GetLayer() + ",\"Rotation\":" + _elementInScene.transform.rotation.eulerAngles.z + ",\"Data\":\"\"}";
        return result;
    }
}