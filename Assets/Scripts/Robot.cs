using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace EnglishKids.Robots {
    public class Robot : MonoBehaviour {
        public Image back;
        [ReadOnly] public RobotSpace robotSpace;
        [ReadOnly] public List<Detail> partsList = new List<Detail>();
        public GameObject animation;

        public void Setup() {
            animation.SetActive(false);
            partsList = GetComponentsInChildren<Detail>(true).ToList();
            foreach (Detail det in partsList) {
                det.robot = this;
                det.index = ConveyorController.Inst.indexPart;
                ConveyorController.Inst.indexPart++;
                det.Setup();
            }
        }

        public int GetCountParts() {
            return partsList.Count;
        }


        public void CheckToWin() {
            if (IsFullRobot()) {
                back.gameObject.SetActive(false);
                foreach (Detail detail in partsList) {
                    detail.gameObject.SetActive(false);
                }

                animation.SetActive(true);
            }

            robotSpace.SetActiveButtonSound(true);
            MainLogic.Inst.CheckWin();
        }

        public bool IsFullRobot() {
            bool isFull = true;

            foreach (Detail detail in partsList) {
                if (!detail.detailOnRobot) {
                    isFull = false;
                    break;
                    ;
                }
            }

            return isFull;
        }
    }
}