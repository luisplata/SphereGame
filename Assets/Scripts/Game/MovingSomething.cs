using System.Collections;
using UnityEngine;
public class MovingSomething : MotionDoing
{
    [SerializeField] private float durationOfLerp;
    private GameObject something;
    private GameObject initial, final;
    private bool isDoing;
    public override void Doing()
    {
        if(!wasConfigured) return;
        StartCoroutine(MoveTransformCoroutine());
        isDoing = true;
    }

    public void Config(GameObject something, GameObject initialTranform, GameObject finalTranform){
        this.something = something;
        initial = initialTranform;
        final = finalTranform;
        wasConfigured = true;
    }

    private IEnumerator MoveTransformCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < durationOfLerp)
        {
            something.transform.position = Vector3.Lerp(initial.transform.position, final.transform.position, elapsedTime / durationOfLerp);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        something.transform.position = final.transform.position;
    }
}