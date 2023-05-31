using System;
using System.Collections;
using UnityEngine;

public class TeleportEnd : BaseElementInScene
{
    private PlayerCustom _player;
    [SerializeField] private float duration;
    [SerializeField] private GameObject pointToExpulse;

    internal void Teleport(PlayerCustom player)
    {
        _player = player;
        _player.transform.position = transform.position;
        StartCoroutine(Expulse());
    }
    
    private IEnumerator Expulse()
    {
        float elapsedTime = 0f;
        var originalPosition = _player.transform.position;
        var targetPosition = pointToExpulse.transform.position;
        while (elapsedTime < duration)
        {
            // Calculate the interpolation value (0 to 1)
            float t = elapsedTime / duration;

            // Move the object from original to target position
            _player.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);

            // Increase the elapsed time by the time since last frame
            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        // Ensure the object reaches the exact target position
        _player.transform.position = targetPosition;

        //Teleport into teleportEnd and start Animation
        Vector2 direction = (transform.position - targetPosition).normalized;
        _player.Shoot(direction);
    }
}