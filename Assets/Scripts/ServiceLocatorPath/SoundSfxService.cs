using System;
using UnityEngine;

namespace ServiceLocatorPath
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundSfxService : MonoBehaviour, ISoundSfxService
    {
        [SerializeField] private ClipCustom[] audioClips;
        [SerializeField] private AudioSource audioSource;
        
        public void Reset()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }

        public void PlaySound(string nameOfSfx)
        {
            if(nameOfSfx.Length == 0) return;

            if (GetAudioClip(nameOfSfx, out var clip))
            {
                audioSource.PlayOneShot(clip);
            }
        }

        private bool GetAudioClip(string nameOfSfx, out AudioClip clip)
        {
            clip = null;
            //get all audios with same name and selected a random one
            var clips = Array.FindAll(audioClips, x => x.NameOfClip == nameOfSfx);
            if (clips.Length > 0)
            {
                clip = clips[UnityEngine.Random.Range(0, clips.Length)].AudioClip;
                return true;
            }
            return false;
        }

        public void PlaySound(string nameOfSfx, bool isRepeating)
        {
            if(nameOfSfx.Length == 0) return;
            if (GetAudioClip(nameOfSfx, out var clip))
            {
                audioSource.clip = clip;
                audioSource.loop = isRepeating;
                audioSource.Play();
            }
        }
    }
}