using System.Collections;
using UnityEngine;

public class PointToStart : BaseElementInScene
{
    [SerializeField] private SpriteRenderer originalColor;
    [SerializeField] private PointToEnd pointToEnd;

    public override void Config(ElementData element, ILogicOfLevel level, bool moveTransform = true)
    {
        base.Config(element, level, moveTransform);
        pointToEnd.Config(element, level);
        ResetAll();
    }

    internal void ShowEndWay(bool isShoot)
    {
        StartCoroutine(ShowEndWayCorrutine(isShoot));
    }

    private IEnumerator ShowEndWayCorrutine(bool isShoot){
        if (!isShoot)
        {
            StartCoroutine(LerpAlphaCoroutine());
        }
        yield return new WaitForSeconds(1);
        pointToEnd.gameObject.SetActive(true);
    }

    private IEnumerator LerpAlphaCoroutine()
    {
        float elapsedTime = 0f;
        float startAlpha = originalColor.color.a;
        var duration = 1;
        var targetAlpha = -1;
        if(startAlpha <=0.5f){
            targetAlpha = 1;
        }else{
            targetAlpha = 0;
        }

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float lerpedAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);

            Color lerpedColor = new Color(originalColor.color.r, originalColor.color.g, originalColor.color.b, lerpedAlpha);
            originalColor.color = lerpedColor;

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Color finalColor = new Color(originalColor.color.r, originalColor.color.g, originalColor.color.b, targetAlpha);
        originalColor.color = finalColor;
    }

    internal void ResetAll()
    {
        pointToEnd.gameObject.SetActive(false);
        originalColor.color = Color.white;
    }

    public PointToEnd GetEndPoint()
    {
        return pointToEnd;
    }
}