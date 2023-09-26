using System;
using UnityEngine;

public class PointToEnd : BaseElementInSceneWithCollider, IPointToEnd {
    private void Start()
    {
        ServiceLocator.Instance.RegisterService<IPointToEnd>(this);
    }

    protected override void OnCollisionEnterBase(GameObject other)
    {
        OnWin?.Invoke();
    }

    public void SetLayer(int currentLayer)
    {
        layer = currentLayer;
    }

    public Action OnWin { get; set; }


}

public interface IPointToEnd
{
    Action OnWin { get; set; }
}