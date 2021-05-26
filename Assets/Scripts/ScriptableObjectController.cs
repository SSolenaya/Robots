using System.Collections;
using System.Collections.Generic;
using EnglishKids.Robots;
using UnityEngine;

public class ScriptableObjectController : Singleton<ScriptableObjectController> {

    public List<RobotShadowSO> shadowSOList = new List<RobotShadowSO>();

    public RobotShadowSO GetRobotShadowSettingByColor(RoboColors color) {
        foreach (var shadow in shadowSOList) {
            if (shadow.color == color) return shadow;
        }
        Debug.LogError("Fail to get robot shadow by its color in SO_Controller");
        return null;
    }
}
