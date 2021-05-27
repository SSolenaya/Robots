using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EnglishKids.Robots;
using UnityEngine;

public class MainLogic : Singleton<MainLogic> {

    public RobotSpace leftSpace;
    public RobotSpace rightSpace;
    [SerializeField] private OptionsController _optController;

    public const RoboColors leftColor = RoboColors.green;       //  may be random
    public const RoboColors rightColor = RoboColors.yellow;

    void Start() {
        leftSpace.Setup(Side.left, leftColor);
        rightSpace.Setup(Side.right, rightColor);

        DOVirtual.DelayedCall(1.5f, () =>ConveyerController.Inst.Setup());
    }

    public void OpenOptions() {
        _optController.Show();
    }
}
