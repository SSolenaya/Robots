using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLogic : MonoBehaviour {

    [SerializeField] private RobotSpace leftSpace;
    [SerializeField] private RobotSpace rightSpace;

    public const RoboColors leftColor = RoboColors.green;       //  may be random
    public const RoboColors rightColor = RoboColors.yellow;

    void Start() {
        leftSpace.Setup(Side.left, leftColor);
        rightSpace.Setup(Side.right, rightColor);

        ConveyerController.Inst.Setup();
    }
}
