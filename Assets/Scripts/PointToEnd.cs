using System;
using UnityEngine;

public class PointToEnd : BaseElementInSceneWithCollider {
    public Action onWin;

    protected override void OnCollisionEnterBase(GameObject other)
    {
        onWin?.Invoke();
    }
}