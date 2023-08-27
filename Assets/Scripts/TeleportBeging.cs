using System.Collections;
using ServiceLocatorPath;
using UnityEngine;

public class TeleportBeging : BaseElementInSceneWithCollider
{
    private PlayerCustom player;
    [SerializeField]private float duration;
    private TeleportEnd _teleportEnd;
    
    public void Config(ElementData element, ILogicOfLevel level, TeleportEnd teleportEnd){
        base.Config(element, level);
        _teleportEnd = teleportEnd;
    }

    protected override void OnCollisionEnterBase(GameObject other)
    {
        if(other.TryGetComponent<PlayerCustom>(out var playerCustom)){
            player = playerCustom;
            playerCustom.Stop();
            StartCoroutine(BlackHole());
            ServiceLocator.Instance.GetService<ISoundSfxService>().PlaySound(sfxName);
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