using System.Collections;
using ServiceLocatorPath;
using UnityEngine;

public class BounceWeak : BaseElementInSceneWithCollider{
    [SerializeField] private float seconsBeforeDestroy;

    protected override void OnCollisionEnterBase(GameObject other)
    {
        StartCoroutine(Destroy());
        ServiceLocator.Instance.GetService<ISoundSfxService>().PlaySound(sfxName);
    }

    private IEnumerator Destroy(){
        yield return new WaitForSeconds(seconsBeforeDestroy);
        //Destroy(gameObject);
        //Disable all components to interact to player
        collision.gameObject.SetActive(false);
    }
}