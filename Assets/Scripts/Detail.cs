using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EnglishKids.Robots {
    public class Detail : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {
        [ReadOnly] public Robot robot;
        [ReadOnly] public int index;
        [ReadOnly] public RectTransform rectTransform;
        [ReadOnly] public bool detailOnRobot;
        [ReadOnly] public bool detailOnDrag;
        [ReadOnly] public bool detailOnConveyor;
        [ReadOnly] public bool canDrag;
        [ReadOnly] public Vector2 conveyerPos; //  позиция на конвеере
        private Vector2 _robotPos; //  позиция при старте - на роботе
        private float _conveyerAngle;
        [ReadOnly] public float radius;
        private float _w;
        private float _h;

        public void Setup() {
            rectTransform = GetComponent<RectTransform>();
            BoxCollider2D boxCol = GetComponent<BoxCollider2D>();
            boxCol.size = new Vector2((1 + SOController.Inst.rs.deltaCollider) * rectTransform.sizeDelta.x, (1 + SOController.Inst.rs.deltaCollider) * rectTransform.sizeDelta.y);
            _h = rectTransform.sizeDelta.y * 0.5f;
            _w = rectTransform.sizeDelta.x * 0.5f;
            radius = Mathf.Sqrt(_h * _h + _w * _w) * SOController.Inst.rs.scaleRobots;
            SetRobotPosition();
            canDrag = true;
        }

        public void SetRobotPosition() {
            _robotPos = rectTransform.anchoredPosition;
        }

        public void SetConveyorPosition(Vector3 pos) {
            rectTransform.localPosition = pos;
            conveyerPos = pos;
        }

        public void SetParentConveyor(Transform tr) {
            rectTransform.SetParent(tr);
        }

        public void SetDetailAngle() {
            float angle = Random.Range(0f, 360f);
            _conveyerAngle = angle;
            transform.Rotate(Vector3.forward, _conveyerAngle);
        }

        public void OnBeginDrag(PointerEventData eventData) {
            if (detailOnRobot || !canDrag) {
                return;
            }

            detailOnDrag = true;
            AudioManager.Inst.PlayPickDetailSound();
            ConveyorController.Inst.canMoveConveyor = false;
            transform.DORotate(Vector3.zero, 0.3f);
            transform.SetParent(ConveyorController.Inst.parentForDetails);
        }

        public void OnDrag(PointerEventData eventData) {
            if (detailOnRobot || !canDrag) {
                return;
            }

            if (eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null) {
                RectTransform draggingPlane = eventData.pointerEnter.transform as RectTransform;
                Vector3 touchPos;
                if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingPlane, eventData.position, eventData.pressEventCamera, out touchPos)) {
                    rectTransform.position = touchPos;
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData) {
            if (detailOnRobot) {
                return;
            }

            canDrag = false;

            float time = 0.5f;
            if (CheckNeedPositionOnRobot()) {
                detailOnRobot = true;
                robot.CheckToWin();
                transform.DOLocalMove(_robotPos, time).OnComplete(() => {
                    detailOnDrag = false;
                    ConveyorController.Inst.canMoveConveyor = true;
                    canDrag = true;
                });
                AudioManager.Inst.PlayCorrectAnswerSound();

                Transform effect = Instantiate(HelpController.Inst.starEffectPrefab);
                effect.position = transform.position;
                DOVirtual.DelayedCall(3, () => {
                    Destroy(effect.gameObject);
                });

            } else {
                AudioManager.Inst.PlayWrongAnswerSound();
                transform.SetParent(ConveyorController.Inst.parentForMoveDetails);
                transform.DOLocalMove(conveyerPos, time).OnComplete(() => {
                    detailOnDrag = false;
                    ConveyorController.Inst.canMoveConveyor = true;
                    canDrag = true;
                });
                transform.DORotate(new Vector3(0, 0, _conveyerAngle), time);
            }
        }

        private bool CheckNeedPositionOnRobot() {
            float delta = (_robotPos - rectTransform.anchoredPosition).magnitude;
            return delta < SOController.Inst.rs.minDetailDis;
        }
    }
}