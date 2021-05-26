using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Detail : MonoBehaviour, IDragHandler, IEndDragHandler
{
   [SerializeField] private Image _img;
   public RectTransform rT;
   private float delta = 0.15f;     //  коэф для увеличения области клика на детали
    private bool flagForMove = false;
    private bool _clickable;
    private Vector3 conveyerPos;
    private float _conveyerAngle;
    public float radius;

    void Start() {
        _clickable = GetComponent<CanvasGroup>().blocksRaycasts;
    }

   public void OnEnable() {
       rT = GetComponent<RectTransform>();
       var boxCol = GetComponent<BoxCollider2D>();
       boxCol.size = new Vector2((1 + delta)*rT.sizeDelta.x, (1 + delta) * rT.sizeDelta.y);
       flagForMove = true;
       radius = (Mathf.Sqrt(rT.sizeDelta.x * rT.sizeDelta.x + rT.sizeDelta.y * rT.sizeDelta.y)) / 2 + delta;
   }

   public void SetDetailAngle(float angle) {
         _conveyerAngle = angle;
         transform.Rotate(Vector3.forward, _conveyerAngle);
    }

   public void SetConveyerPos(Vector3 pos) {

        rT.anchoredPosition = pos;
        conveyerPos = rT.localPosition;
   }

   public void SetSpriteToDetail(RoboColors color, RoboParts detailName) {
       string path = "Sprites/" + color.ToString() + "/" + detailName.ToString();
       var spr = Resources.Load<Sprite>(path);      
       try {
           _img.sprite = spr;
           _img.SetNativeSize();        //  придание Image размеров внутреннего спрайта
       } catch {
           Debug.Log("Error in sprite attaching to detail: " + color + " " + detailName);
       }
   }

 public void OnDrag(PointerEventData eventData) {
     if (flagForMove && eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null) {
            //conveyerPos = rT.anchoredPosition;
            _clickable = false;
            var draggingPlane = eventData.pointerEnter.transform as RectTransform;
         Vector3 touchPos;
         transform.DORotate(Vector3.zero, 1f, RotateMode.Fast);
         if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingPlane, eventData.position, eventData.pressEventCamera, out touchPos))
             rT.position = touchPos;

     }
   }

 public void OnEndDrag(PointerEventData eventData) {
     if (flagForMove) {
         transform.DOLocalMove(conveyerPos, 1f);
         transform.DORotate(new Vector3(0, 0, _conveyerAngle), 1.5f, RotateMode.Fast);
         _clickable = true;
     }
 }

 void OnDisable() {
     flagForMove = false;
    }
}
