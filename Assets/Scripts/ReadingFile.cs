using System.Collections.Generic;
using UnityEngine;

public class ReadingFile
{
    private ElementDataList elementDataList;
    public ReadingFile(string mapToLoad)
    {
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
    public int PositionX;
    public int PositionY;
    public int Layer;
    public int Rotation;
}

[System.Serializable]
public class ElementDataList
{
    public ElementData[] elements;
}
