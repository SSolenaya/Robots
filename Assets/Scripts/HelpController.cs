using System;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.Robots {
    public class HelpController : Singleton<HelpController> {
        public Transform starEffectPrefab;
        public List<BackInfo> listBackInfo;
        public List<RobotInfo> listRobotInfo;

        public Sprite GetBackByRoboColorsAndSide(RoboColors roboColors, Side side) {
            foreach (BackInfo backInfo in listBackInfo) {
                if (backInfo.side == side && backInfo.robotColors == roboColors) {
                    return backInfo.backSprite;
                }
            }

            Debug.Log("Error GetBackByRoboColors " + roboColors);
            return null;
        }

        public Robot GetRobotByColor(RoboColors roboColors) {
            foreach (RobotInfo robotInfo in listRobotInfo) {
                if (robotInfo.robotColors == roboColors) {
                    return robotInfo.robot;
                }
            }

            Debug.Log("Error GetRobotByColor " + roboColors);
            return null;
        }
    }

    public enum RoboColors {
        green,
        yellow
    }

    public enum Side {
        left,
        right
    }

    [Serializable]
    public class BackInfo {
        public Sprite backSprite;
        public RoboColors robotColors;
        public Side side;
    }

    [Serializable]
    public class RobotInfo {
        public Robot robot;
        public RoboColors robotColors;
    }
}