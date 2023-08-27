using UnityEngine;
using Object = UnityEngine.Object;

namespace ServiceLocatorPath
{
    public class InstallerServiceLocator : MonoBehaviour
    {
        [SerializeField, InterfaceType(typeof(ISoundSfxService))]
        private Object soundSfxService;
        private ISoundSfxService SoundSfxService => soundSfxService as ISoundSfxService;
        
        /* Template for install services
        [SerializeField, InterfaceType(typeof(ISoundSfxService))]
        private Object soundSfxService;
        private ISoundSfxService SoundSfxService => soundSfxService as ISoundSfxService;*/

        private void Awake()
        {
            ServiceLocator.Instance.RegisterService(SoundSfxService);
        }
    }
}