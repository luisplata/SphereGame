using System;
using UnityEngine;

public class Bounce : BaseElementInScene{

    public CheckPoint CheckPoint => checkPoint;
    [SerializeField] private CheckPoint checkPoint;

    public void Config(ElementData element, Action successPoint, ILogicOfLevel levelLogic)
    {
        Config(element);
        checkPoint.Config(element.Layer, levelLogic);
        checkPoint.onSuccessPoint = successPoint;
    }
}