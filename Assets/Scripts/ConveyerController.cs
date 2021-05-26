using System.Collections.Generic;
using DG.Tweening;
using EnglishKids.Robots;
using UnityEngine;

public class ConveyerController : Singleton<ConveyerController> {
    [SerializeField] private Detail _detailPrefab;

    //private List<Detail> _leftDetailsList = new List<Detail>();
    //private List<Detail> _rightDetailsList = new List<Detail>();
    [SerializeField] private Transform _parentForEnable;
    [SerializeField] private Transform _movingTape;
    private RectTransform _rT;
    private float currentY; //bear temp
    private int detailsCount;
    public Camera mainCam;
    private float _speed = 250f;
    [SerializeField] private bool _flagForTapeMoving;

    public void Setup() {
        _rT = gameObject.GetComponent<RectTransform>();
        currentY = _rT.sizeDelta.y;
        detailsCount = MainLogic.Inst.leftSpace.roboContour.partsList.Count + MainLogic.Inst.rightSpace.roboContour.partsList.Count;
        FillConveyer();
        DOVirtual.DelayedCall(1f, () => _flagForTapeMoving = true);
    }

    void Update() {
        if (_flagForTapeMoving) {

            _movingTape.localPosition += Vector3.down * _speed * Time.deltaTime;
        }
    }

    public void MovingTape(int step) {
        float delta = step * _rT.sizeDelta.y;
        _movingTape.DOLocalMoveY(-(delta * 1.5f), 5f);
    }

    public void TapeMovingStatus(bool var) {
        _flagForTapeMoving = var;
    }

    private void FillConveyer() {

        CreateArray();
        for (int i = 0; i < detailsCount; i++) {
            Detail det = GetRandomDetail();
            float angle = Random.Range(-180, 180);
            det.SetDetailAngle(angle);
            PlaceDetailOnConveyer(det);
        }
    }

    private void PlaceDetailOnConveyer(Detail det) {
        det.transform.SetParent(_parentForEnable);
        det.gameObject.SetActive(true);
        float detSize = det.radius;
        float delta = _rT.sizeDelta.x/2 - detSize;
        float localX = Random.Range(-delta, delta);
        currentY += detSize*2;
        float localY = currentY;
        det.SetConveyerPos(new Vector3(localX, localY, 0));
        det.SetParentConveyer(_parentForEnable);
    }

    public List<Detail> randomMassiv = new List<Detail>();
    public List<int> indexArray = new List<int>();

    private void CreateArray() {
        int i = 0;
        foreach (var e in MainLogic.Inst.leftSpace.roboContour.partsList)
        {
            randomMassiv.Add(e);
            indexArray.Add(i);
            i++;
        }

        foreach (var e in MainLogic.Inst.rightSpace.roboContour.partsList)
        {
            randomMassiv.Add(e);
            indexArray.Add(i);
            i++;
        }


    }
    private Detail GetRandomDetail() {

        int r = Random.Range(0, indexArray.Count);
        int iR = indexArray[r];
        indexArray.RemoveAt(r);
   
        return randomMassiv[iR];
    }
}