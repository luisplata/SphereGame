using ServiceLocatorPath;
using UnityEngine;

public class Bounce : BaseElementInSceneWithCollider
{
    protected override void OnCollisionEnterBase(GameObject other)
    {
        ServiceLocator.Instance.GetService<ISoundSfxService>().PlaySound(sfxName);
    }
}