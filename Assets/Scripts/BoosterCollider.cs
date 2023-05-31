using UnityEngine;

public class BoosterCollider : CollisionCustom{
    [SerializeField] private GameObject target, middle;

    internal GameObject GetMiddle()
    {
        return middle;
    }

    internal GameObject GetTarget()
    {
        return target;
    }
}