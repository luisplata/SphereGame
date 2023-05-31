using System.Collections.Generic;
using UnityEngine;

public interface ILogicOfLevel
{
    int GetCurrentLayer();
    List<GameObject> GetElements(string data);
}