using UnityEngine;
using UnityEngine.UI;

namespace EnglishKids.Robots {
    public class RobotSpace : MonoBehaviour {
        [SerializeField] private RectTransform _pointForRobot;
        [SerializeField] private Image _background;
        [SerializeField] [ReadOnly] private Side _side;
        [SerializeField] [ReadOnly] private RoboColors _color;
        [ReadOnly] public Robot robot;
        [SerializeField] private VoiceButton _voiceButton;

        public void Setup(Side side, RoboColors color) {
            _side = side;
            _color = color;
            _background.sprite = HelpController.Inst.GetBackByRoboColorsAndSide(_color, _side);
            SetRobotByColor(color);
            _voiceButton.Setup(color);
            SetActiveButtonSound(false);
        }

        private void SetRobotByColor(RoboColors color) {
            robot = Instantiate(HelpController.Inst.GetRobotByColor(color));
            robot.transform.SetParent(transform);
            robot.GetComponent<RectTransform>().anchoredPosition3D = _pointForRobot.anchoredPosition3D;
            robot.transform.localScale = Vector3.one * SOController.Inst.rs.scaleRobots;
            robot.Setup();
            robot.robotSpace = this;
            robot.gameObject.SetActive(true);
        }

        public int GetCountParts() {
            return robot.GetCountParts();
        }

        public void SetActiveButtonSound(bool var) {
            _voiceButton.gameObject.SetActive(var);
            if (var) {
                _voiceButton.PlayColorWord();
            }
        }
    }
}