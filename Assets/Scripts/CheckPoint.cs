using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Action onSuccessPoint;
    private ILogicOfLevel _levelLogic;

    public int Layer { get; internal set; }

    internal void Config(int layer, ILogicOfLevel levelLogic)
    {
        _levelLogic = levelLogic;
        Layer = layer;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if(Layer == _levelLogic.GetCurrentLayer()){
                onSuccessPoint?.Invoke();
            }
        }
    }
}