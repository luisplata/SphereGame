using System.Collections;
using UnityEngine;

public class TeleportBeging : BaseElementInScene
{
    private PlayerCustom player;
    [SerializeField]private float duration;
    private TeleportEnd _teleportEnd;

    public void Config(ElementData element, TeleportEnd teleportEnd){
        base.Config(element);
        _teleportEnd = teleportEnd;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            if(other.gameObject.TryGetComponent<PlayerCustom>(out var playerCustom)){
                player = playerCustom;
                playerCustom.Stop();
                StartCoroutine(BlackHole());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            player = null;
        }
    }

    private IEnumerator BlackHole()
    {
        float elapsedTime = 0f;
        var originalPosition = player.transform.position;
        var targetPosition = transform.position;
        while (elapsedTime < duration)
        {
            // Calculate the interpolation value (0 to 1)
            float t = elapsedTime / duration;

            // Move the object from original to target position
            player.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);

            // Increase the elapsed time by the time since last frame
            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        // Ensure the object reaches the exact target position
        player.transform.position = targetPosition;

        //Teleport into teleportEnd and start Animation
        _teleportEnd.Teleport(player);
        player = null;
    }
}