using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.Robots {

    public class AudioManager : Singleton<AudioManager> {
        [SerializeField] private AudioSource _effects;
        [SerializeField] private AudioSource _voice;
        [SerializeField] private AudioSource _music;

        [SerializeField] private AudioClip _conveyerSound;     //  Конвейер начинает и заканчивает движение
        [SerializeField] private AudioClip _pickDetail;        //   Берем деталь с ленты
        [SerializeField] private AudioClip _wrongAnswer;        //   Пытаемся поставить в неправильный слот
        [SerializeField] private AudioClip _correctAnswer;      //  правильный слот


        void Start() {
            OptionsController.Inst.effectSliderAction += SetEffectsVolumeTo;
            OptionsController.Inst.voiceSliderAction += SetVoiceVolumeTo;
            OptionsController.Inst.musicSliderAction += SetMusicVolumeTo;
        }

        public void PlayConveyerSound() {
            _effects.PlayOneShot(_conveyerSound);
        }

        public void PlayPickDetailSound()
        {
            _effects.PlayOneShot(_pickDetail);
        }

        public void PlayWrongAnswerSound()
        {
            _effects.PlayOneShot(_wrongAnswer);
        }

        public void PlayCorrectAnswerSound()
        {
            _effects.PlayOneShot(_correctAnswer);
        }

        private void SetEffectsVolumeTo(float newVolume) {
            if (newVolume < 0 || newVolume > 1) {
                Debug.LogError("Uncorrect volume");
            }
            _effects.volume = newVolume;
        }

        private void SetVoiceVolumeTo(float newVolume)
        {
            if (newVolume < 0 || newVolume > 1)
            {
                Debug.LogError("Uncorrect volume");
            }
            _voice.volume = newVolume;
        }

        private void SetMusicVolumeTo(float newVolume)
        {
            if (newVolume < 0 || newVolume > 1)
            {
                Debug.LogError("Uncorrect volume");
            }
            _music.volume = newVolume;
        }
    }
}
