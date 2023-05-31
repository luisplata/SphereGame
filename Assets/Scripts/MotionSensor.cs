using System;
using UnityEngine;
public class MotionSensor : BaseElementInSceneWithCollider {
    [SerializeField] private MotionDoing doing;
    public override void Config(ElementData element, ILogicOfLevel level)
    {
        base.Config(element, level);
    }
    protected override void OnCollisionEnter(GameObject other)
    {
        throw new NotImplementedException();
    }
}