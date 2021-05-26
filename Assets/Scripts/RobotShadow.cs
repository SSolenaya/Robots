using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum RoboParts {
    head,
    body,
    hand_l,
    hand_r,
    leg_l,
    leg_r
}

public class RobotShadow : MonoBehaviour {

    public RoboColors color;

    public List<Detail> partsList = new List<Detail>();

    public void Setup() {
        var children = GetComponentsInChildren<Detail>(true).ToList();
        foreach (var det in children) {
            partsList.Add(det);
        }

        foreach (var det in partsList) {
            det.Setup(this.transform);
        }
    }
}
