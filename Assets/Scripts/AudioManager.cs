using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

namespace EnglishKids.Robots {
    public class AudioManager : Singleton<AudioManager> {
        [SerializeField] private AudioSource _effectsConveyor;
        [SerializeField] private AudioSource _effects;
        [SerializeField] private AudioSource _voice;
        [SerializeField] private AudioSource _music;

        [SerializeField] private AudioClip _conveyerSound; //  Конвейер начинает и заканчивает движение
        [SerializeField] private AudioClip _pickDetail; //   Берем деталь с ленты
        [SerializeField] private AudioClip _wrongAnswer; //   Пытаемся поставить в неправильный слот
        [SerializeField] private AudioClip _correctAnswer; //  правильный слот

        [SerializeField] private AudioClip _voiceGreen; //  звук "зелёный"
        [SerializeField] private AudioClip _voiceYellow; //  звук "желтый"


        [ReadOnly] public List<AudioClip> listEffectsToPlay;//лист для контроля очереди звука

        public void PlayConveyorSound(bool var) {
            if (!var) {
                _effectsConveyor.Stop();
                return;
            }

            if (_effectsConveyor.isPlaying) {
                return;
            }

            _effectsConveyor.clip = _conveyerSound;
            _effectsConveyor.loop = true;
            _effectsConveyor.Play();
        }

        [Button]
        public void PlayPickDetailSound() {
            _effects.PlayOneShot(_pickDetail, 1);
        }

        public void PlayWrongAnswerSound() {
            _effects.PlayOneShot(_wrongAnswer);
        }

        public void PlayCorrectAnswerSound() {
            _effects.PlayOneShot(_correctAnswer);
        }

        public void PlayVoiceByColor(RoboColors roboColors) {
            listEffectsToPlay.Clear();
            switch (roboColors) {
                case RoboColors.green:
                    listEffectsToPlay.Add(_voiceGreen);
                    break;
                case RoboColors.yellow:
                    listEffectsToPlay.Add(_voiceYellow);
                    break;
            }
        }
        
        public void Update() {
            if (!_voice.isPlaying && listEffectsToPlay.Count > 0) {
                AudioClip a = listEffectsToPlay[0];
                listEffectsToPlay.RemoveAt(0);
                _voice.PlayOneShot(a);
            }
        }
    }
}