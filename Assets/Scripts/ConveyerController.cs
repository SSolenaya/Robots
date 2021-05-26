using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EnglishKids.Robots;
using UnityEngine;
using DG.Tweening;

public class ConveyerController : Singleton<ConveyerController> {

    [SerializeField] private Detail _detailPrefab;
    private List<Detail> _leftDetailsList = new List<Detail>();
    private List<Detail> _rightDetailsList = new List<Detail>();
    [SerializeField] private Transform _parentForUnenable;
    [SerializeField] private Transform _parentForEnable;
    [SerializeField] private Transform _movingTape;
    private RectTransform _rT;
    private float currentY = 0; //bear temp
    private int detailsCount;
    public Camera mainCam;

    public void Setup() {
        _rT = gameObject.GetComponent<RectTransform>();
        var leftInfo = ScriptableObjectController.Inst.GetRobotShadowSettingByColor(MainLogic.leftColor);
        FillDetailsLists(_leftDetailsList, leftInfo);
        var rightInfo = ScriptableObjectController.Inst.GetRobotShadowSettingByColor(MainLogic.rightColor);
        FillDetailsLists(_rightDetailsList, rightInfo);
        detailsCount = _leftDetailsList.Count + _rightDetailsList.Count;
        FillConveyer();
        DOVirtual.DelayedCall(2f, MovingTape);
    }

    public void MovingTape() {
        var step = Screen.height - 100;
        _movingTape.DOMoveY(-step, 5f, false);
    }

    private void FillConveyer() {
        
        for (int i=0; i< detailsCount - 1; i++) {
            var det = GetRandomDetail();
            float angle = Random.Range(-180, 180);
            det.SetDetailAngle(angle);
            PlaceDetailOnConveyer(det);
        } 
    }

    private void PlaceDetailOnConveyer(Detail det) {
        det.transform.SetParent(_parentForEnable);
        det.gameObject.SetActive(true);
        var detSize = det.radius;
        var delta = (_rT.sizeDelta.x - detSize * 2) / 2;
        var localX = Random.Range(-delta, delta);
        var localY = currentY + detSize;
        currentY = localY + detSize;
        det.SetConveyerPos(new Vector3(localX, localY, 0));
        //det.v2 = new Vector2(localX, localY);
    }

    private void FillDetailsLists(List<Detail> currentList, RobotShadowSO settings) {
        foreach (var part in settings.partsList) {
            var det = Instantiate(_detailPrefab, _parentForUnenable);
            det.SetSpriteToDetail(settings.color, part);
            det.gameObject.SetActive(false);
            currentList.Add(det);
        }
    }

    private Detail GetRandomDetail() {
        float r = Random.Range(0f, 1.1f);
        var currentList = r > 0.5f ? _leftDetailsList : _rightDetailsList;
        if (currentList.Count == 0) {
            currentList = _leftDetailsList.Count == 0 ? _rightDetailsList : _leftDetailsList;
        }
        int i = Random.Range(0, currentList.Count);
        var result = currentList[i];
        currentList.RemoveAt(i);
        return result;
    }

}
