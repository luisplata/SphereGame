using System;
using UnityEngine;

public class LogicForMainMechanic : MonoBehaviour
{
    public Action onStartShoot;
    [SerializeField] private PlayerCustom player;
    [SerializeField] private InputFacade input;
    private PlayerCustom playerInstantiate;
    private void Start() {
        input.onRelease = ShootPlayer;
        input.onFirstPosition = LocatePlayer;
    }

    private void LocatePlayer(Vector2 vector)
    {
        playerInstantiate = Instantiate(player);
        playerInstantiate?.Locate(vector);
        onStartShoot?.Invoke();
    }

    private void ShootPlayer(Vector2 direction)
    {
        playerInstantiate?.Shoot(direction);
        Destroy(playerInstantiate.gameObject, 20);
    }
}