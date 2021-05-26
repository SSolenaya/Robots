using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.Robots {

    public class AudioManager : Singleton<AudioManager> {
        private List<AudioSource> _effects;
        private List<AudioSource> _voice;
        private List<AudioSource> _music;

        void Start() {
            OptionsController.Inst.effectSliderAction += SetEffectsVolumeTo;
            OptionsController.Inst.voiceSliderAction += SetVoiceVolumeTo;
            OptionsController.Inst.musicSliderAction += SetMusicVolumeTo;
    }

        private void SetEffectsVolumeTo(float newVolume) {
            if (newVolume < 0 || newVolume > 1) {
                Debug.LogError("Uncorrect volume");
            }

            foreach (var eff in _effects) {
                eff.volume = newVolume;
            }
        }

        private void SetVoiceVolumeTo(float newVolume)
        {
            if (newVolume < 0 || newVolume > 1)
            {
                Debug.LogError("Uncorrect volume");
            }

            foreach (var voi in _voice)
            {
                voi.volume = newVolume;
            }
        }

        private void SetMusicVolumeTo(float newVolume)
        {
            if (newVolume < 0 || newVolume > 1)
            {
                Debug.LogError("Uncorrect volume");
            }

            foreach (var mus in _effects)
            {
                mus.volume = newVolume;
            }
        }
    }
}
