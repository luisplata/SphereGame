using UnityEngine;

namespace ServiceLocatorPath
{
    [CreateAssetMenu(menuName = "Create ClipCustom", fileName = "ClipCustom", order = 0)]
    internal class ClipCustom : ScriptableObject
    {
        [SerializeField] private string nameOfClip;
        [SerializeField] private AudioClip clip;
        public string NameOfClip => nameOfClip;
        public AudioClip AudioClip => clip;
    }
}