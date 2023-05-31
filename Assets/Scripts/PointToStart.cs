using System;
using System.Collections;
using UnityEngine;

public class PointToStart : BaseElementInScene
{
    public Action onWin;
    [SerializeField] private PointToEnd end;
    [SerializeField] private SpriteRenderer originalColor;

    public override void Config(ElementData element, ILogicOfLevel level)
    {
        base.Config(element, level);
        end.Config(element, level);
        end.onWin = ()=>{
            onWin?.Invoke();
        };
        ResetAll();
    }

    internal void ShowEndWay()
    {
        StartCoroutine(ShowEndWayCorrutine());
    }

    private IEnumerator ShowEndWayCorrutine(){
        StartCoroutine(LerpAlphaCoroutine());
        yield return new WaitForSeconds(1);
        end.gameObject.SetActive(true);
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
        end.gameObject.SetActive(false);
        originalColor.color = Color.white;
    }
}