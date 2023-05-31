using System;
using UnityEngine;

public class PointToEnd : BaseElementInSceneWithCollider {
    public Action onWin;

    protected override void OnCollisionEnter(GameObject other)
    {
        onWin?.Invoke();
    }
}