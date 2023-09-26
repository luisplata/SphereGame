using UnityEngine;
using Object = UnityEngine.Object;

namespace ServiceLocatorPath
{
    public class InstallerServiceLocator : MonoBehaviour
    {
        [SerializeField, InterfaceType(typeof(ISoundSfxService))]
        private Object soundSfxService;
        [SerializeField, InterfaceType(typeof(ILogicOfLevel))]
        private Object logicOfLevelService;
        private ISoundSfxService SoundSfxService => soundSfxService as ISoundSfxService;
        private ILogicOfLevel Logic => logicOfLevelService as ILogicOfLevel;
        
        /* Template for install services
        [SerializeField, InterfaceType(typeof(ISoundSfxService))]
        private Object soundSfxService;
        private ISoundSfxService SoundSfxService => soundSfxService as ISoundSfxService;*/

        private void Awake()
        {
            ServiceLocator.Instance.RegisterService(SoundSfxService);
            ServiceLocator.Instance.RegisterService(Logic);
        }
    }
}