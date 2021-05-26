using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RobotShadowSettings", menuName = "ScriptableObjects/Create robot shadow settings")]
public class RobotShadowSO : ScriptableObject {

    public RoboColors color;

    public List<RoboParts> partsList = new List<RoboParts>();
}
