using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Detail : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
   public RectTransform rT;
   private float deltaCollider = 0.15f;     //  коэф для увеличения области клика на детали
   private float deltaMatch = 0.1f;          //  допуск несоответствия позиций детали и контура - 10%
    private bool flagForMove = false;
    private bool _clickable;
    private Vector3 conveyerPos;    //  позиция на конвеере
    private Vector3 robotPos;       //  позиция при старте - на роботе
    private float _conveyerAngle;
    public float radius;
    private Transform _parentRobot;
    private Transform _parentConveyer;

   public void Setup(Transform tr) {
        rT = GetComponent<RectTransform>();
        _clickable = GetComponent<CanvasGroup>().blocksRaycasts;
        var boxCol = GetComponent<BoxCollider2D>();
        boxCol.size = new Vector2((1 + deltaCollider) * rT.sizeDelta.x, (1 + deltaCollider) * rT.sizeDelta.y);
        flagForMove = true;
        radius = (Mathf.Sqrt(rT.sizeDelta.x * rT.sizeDelta.x + rT.sizeDelta.y * rT.sizeDelta.y)) / 2 + deltaCollider;

        SetRobotPosition();
        SetParentRobot(tr);
   }

    public void SetRobotPosition() {
        robotPos = rT.anchoredPosition;
    }

    public void SetParentRobot(Transform pRTransform) {
        _parentRobot = pRTransform;
    }

    public void SetParentConveyer(Transform convTransform)
    {
        _parentConveyer = convTransform;
    }
  public void SetDetailAngle(float angle) {
         _conveyerAngle = angle;
         transform.Rotate(Vector3.forward, _conveyerAngle);
    }

   public void SetConveyerPos(Vector3 pos) {
        rT.anchoredPosition = pos;
        conveyerPos = rT.localPosition;
   }

  
 public void OnDrag(PointerEventData eventData) {
     if (flagForMove && eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null) {
            //conveyerPos = rT.anchoredPosition;
            
            var draggingPlane = eventData.pointerEnter.transform as RectTransform;
         Vector3 touchPos;
         if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingPlane, eventData.position, eventData.pressEventCamera, out touchPos))
             rT.position = touchPos;

     }
   }

 public void OnEndDrag(PointerEventData eventData) {
     if (!CheckForMatch()) {
         transform.SetParent(_parentConveyer);
         transform.DOLocalMove(conveyerPos, 1f);
         transform.DORotate(new Vector3(0, 0, _conveyerAngle), 1.5f, RotateMode.Fast);
         _clickable = true;
     } else {
         transform.DOLocalMove(robotPos, 0.2f);
     }
 }

 private bool CheckForMatch() {
     var delta = robotPos - transform.localPosition;
     return (delta.y < robotPos.y * deltaMatch) && (delta.x < robotPos.x * deltaMatch);
 }

 void OnDisable() {
     flagForMove = false;
    }

 public void OnBeginDrag(PointerEventData eventData) {
     _clickable = false;
     transform.DORotate(Vector3.zero, 1f, RotateMode.Fast);
     transform.SetParent(_parentRobot);
 }
}
