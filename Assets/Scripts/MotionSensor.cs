using System;
using UnityEngine;
public class MotionSensor : BaseElementInScene {

    public CheckPoint CheckPoint => checkPoint;
    [SerializeField] private CheckPoint checkPoint;
    [SerializeField] private MotionDoing doing;
    private ILogicOfLevel _level;

    public void Config(ElementData element, ILogicOfLevel level)
    {
        base.Config(element);
        ConfigElements(element.Data);
        _level = level;
        checkPoint.Config(GetLayer(), level);
        checkPoint.onSuccessPoint = ()=>{
            doing.Doing();
        };
    }

    private void ConfigElements(string data)
    {
        Debug.Log($"Data {data}");
        var elements = _level.GetElements(data);
        var movemnt = (MovingSomething)doing;
    }
}