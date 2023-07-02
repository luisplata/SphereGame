using System.Collections;
using UnityEngine;

public class BounceWeak : BaseElementInSceneWithCollider{
    [SerializeField] private float seconsBeforeDestroy;

    protected override void OnCollisionEnterBase(GameObject other)
    {
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy(){
        yield return new WaitForSeconds(seconsBeforeDestroy);
        Destroy(gameObject);
    }
}