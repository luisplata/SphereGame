using System.Collections.Generic;
using UnityEngine;

public abstract class MotionDoing : MonoBehaviour{
    protected List<BaseElementInScene> _elementsFromSensor;
    protected bool wasConfigured;

    public abstract void Doing();

    public virtual void Config(List<BaseElementInScene> elementsFromSensor)
    {
        _elementsFromSensor = elementsFromSensor;
        wasConfigured = true;
    }
}