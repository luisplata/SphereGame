using System.Collections.Generic;
using UnityEngine;
public class MotionSensor : BaseElementInSceneWithCollider {
    [SerializeField] private MotionDoing doing;
    protected override void OnCollisionEnterBase(GameObject other)
    {
        doing.Doing();
    }

    public void Config(ElementData element, LevelLogic level, List<BaseElementInScene> elementsFromSensor)
    {
        base.Config(element, level);
        doing.Config(elementsFromSensor);
    }
}