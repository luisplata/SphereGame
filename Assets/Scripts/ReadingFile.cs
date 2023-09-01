using System.Collections.Generic;
using UnityEngine;

public class ReadingFile
{
    private ElementDataList elementDataList;
    public ReadingFile(string mapToLoad)
    {
        Debug.Log($"|||||{mapToLoad}");
        elementDataList = JsonUtility.FromJson<ElementDataList>(mapToLoad);
    }

    internal IEnumerable<ElementData> GetElements()
    {
        return elementDataList.elements;
    }
}

[System.Serializable]
public class ElementData
{
    public string Element;
    public float PositionX;
    public float PositionY;
    public int Layer;
    public int Rotation;
    public string Data;
    public int LayerDestiny;
}

[System.Serializable]
public class ElementDataList
{
    public ElementData[] elements;
}
