using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace paper
{
    public class AudioManager : SingletonDDOl<AudioManager>
    {

        public AudioSource source;

        private void Start()
        {
            source = GetComponent<AudioSource>();
        }
        public void playSFX(So_Sfx sfx)
        {
            source.volume = sfx.volume;
            source.pitch = sfx.pitch;
            source.panStereo = sfx.stereoPan;
            source.priority = sfx.priority;
            source.spatialBlend = sfx.spatialBlend;
            source.reverbZoneMix = sfx.reverbZoneMix;

            source.PlayOneShot(sfx.clip, 1);
        }
    }
}