using System;
using System.Collections;
using ServiceLocatorPath;
using UnityEngine;

public class Booster : BaseElementInSceneWithCollider {
    [SerializeField] private BoosterCollider colliders;
    [SerializeField] private float duration;
    private bool canUse = true;
    
    protected override void OnCollisionEnterBase(GameObject other){
        StartCoroutine(MoveWithCurveCoroutine(other.GetComponent<PlayerCustom>(), colliders.GetMiddle(), colliders.GetTarget(),duration));
        ServiceLocator.Instance.GetService<ISoundSfxService>().PlaySound(sfxName);
    }
    
    private IEnumerator MoveWithCurveCoroutine(PlayerCustom startPosition, GameObject middlePosition, GameObject targetPosition, float duration)
    {
        if(canUse){
            canUse = false;
            startPosition.Stop();
            float elapsedTime = 0f;

            Vector2 startPositionn = startPosition.transform.position;
            Vector2 targetPositionn = targetPosition.transform.position;
            Vector2 middlePositionn = middlePosition.transform.position;

            while (elapsedTime < duration)
            {
                try{
                    // Calculate the interpolation value (0 to 1)
                    float t = elapsedTime / duration;

                    // Calculate the positions along the curve using Bezier interpolation
                    Vector2 curvePosition = CalculateBezierPoint(startPositionn, middlePositionn, targetPositionn, t);

                    // Set the object's position to the current position on the curve
                    startPosition.transform.position = curvePosition;

                    // Increase the elapsed time by the time since the last frame
                    elapsedTime += Time.deltaTime;
                }catch(Exception){
                    break;
                }
                yield return null; // Wait for the next frame
            }
            try{
                // Ensure the object reaches the exact target position
                startPosition.transform.position = targetPosition.transform.position;
                Vector2 direction = (middlePosition.transform.position - targetPosition.transform.position).normalized;
                startPosition.Shoot(direction);
            }catch(Exception){
                // Reset the booster
            }
            canUse = true;
        }
    }

    private Vector2 CalculateBezierPoint(Vector2 p0, Vector2 p1, Vector2 p2, float t)
    {
        // Bezier curve formula: B(t) = (1 - t)^2 * P0 + 2 * (1 - t) * t * P1 + t^2 * P2
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;

        Vector2 point = uu * p0 + 2f * u * t * p1 + tt * p2;
        return point;
    }
}