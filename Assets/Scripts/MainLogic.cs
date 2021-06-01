using DG.Tweening;
using EasyButtons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EnglishKids.Robots {
    public class MainLogic : Singleton<MainLogic> {
        public RobotSpace leftSpace;
        public RobotSpace rightSpace;

        public RectTransform leftCorner;
        public RectTransform rightCorner;

        private void Start() {
            leftSpace.Setup(Side.left, RoboColors.yellow);
            rightSpace.Setup(Side.right, RoboColors.green);
            ConveyorController.Inst.Setup();
        }

        public void CheckWin() {
            bool leftRobot = leftSpace.robot.IsFullRobot();
            bool rightRobot = rightSpace.robot.IsFullRobot();


            if (leftRobot && rightRobot) {
                for (int i = 0; i < 10; i++) {
                    DOVirtual.DelayedCall(i * Random.Range(0.1f, 0.2f), () => { StarsEffect(); });
                }

                DOVirtual.DelayedCall(5, () => { LoadMainMenu(); });
            }
        }

        [Button]
        public void StarsEffect() {
            Transform effect = Instantiate(HelpController.Inst.starEffectPrefab);
            effect.position = GetRandomPos();
        }

        public Vector3 GetRandomPos() {
            float x = Mathf.Lerp(leftCorner.position.x, rightCorner.position.x, Random.value);
            float y = Mathf.Lerp(leftCorner.position.y, rightCorner.position.y, Random.value);
            return new Vector3(x, y, leftCorner.position.z);
        }

        public void LoadMainMenu() {
            SceneManager.LoadScene(0);
        }
    }
}