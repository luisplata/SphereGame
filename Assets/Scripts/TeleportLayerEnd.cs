using System.Collections;
using ServiceLocatorPath;
using UnityEngine;

public class TeleportLayerEnd : TeleportEnd
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private int _layerDestiny;
    private InputFacade _inputFacade;

    public void Config(ElementData element, ILogicOfLevel level, int layerDestiny, InputFacade inputFacade)
    {
        base.Config(element, level);
        layer = layerDestiny;
        _inputFacade = inputFacade;
    }

    internal override void Teleport(PlayerCustom player)
    {
        _player = player;
        _player.transform.position = transform.position;
        _player.Stop();
        ServiceLocator.Instance.GetService<ISoundSfxService>().PlaySound(sfxName);
        StartCoroutine(AddTimeToPlayer(player, 2f));
    }

    private IEnumerator AddTimeToPlayer(PlayerCustom playerCustom, float time)
    {
        while (!playerCustom.CanMove())
        {
            yield return new WaitForSeconds(time);
            playerCustom.AddTime(time);   
        }
    }
}