using UnityEngine;
using UnityEngine.UI;

namespace EnglishKids.Robots {
    public class VoiceButton : MonoBehaviour {
        [SerializeField] private Button _button;
        [SerializeField] private Text _buttonText;
        private RoboColors color;

        public void Setup(RoboColors col) {
            color = col;
            SetVoiceButtonText(color);
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(PlayColorWord);
        }

        private void SetVoiceButtonText(RoboColors col) {
            _buttonText.text = col.ToString().ToUpper();
        }

        public void PlayColorWord() {
            AudioManager.Inst.PlayVoiceByColor(color);
        }
    }
}