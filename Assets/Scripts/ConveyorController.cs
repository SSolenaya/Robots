using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.Robots {
    public class ConveyorController : Singleton<ConveyorController> {
        public TapeMovingDetector tapeMovingDetector; //Индикатор детали внизу конвеера
        public RectTransform upPoint; // точка для позиционирования на верхней части экрана
        public RectTransform parentForMoveDetails; //родитель на конвеере
        public RectTransform parentForDetails; //родитель вне конеера
        [SerializeField] private Transform _movingTape;
        private float _currentYforDetail;
        [ReadOnly] public bool canMoveConveyor;
        [ReadOnly] public List<Detail> randomMassiv = new List<Detail>();
        [ReadOnly] public List<int> indexArray = new List<int>();

        public RectTransform leftDownPoint; //углы экрана канваса
        public RectTransform rightUpPoint; //углы экрана канваса

        [ReadOnly] public bool haveCollision;
        [ReadOnly] public int indexPart;

        public void Setup() {
            tapeMovingDetector.actionHaveCollision += var => { haveCollision = var; };
            upPoint.SetParent(parentForMoveDetails);
            _currentYforDetail = upPoint.anchoredPosition.y;
            FillConveyor();
            canMoveConveyor = true;
        }

        private void FillConveyor() {
            float _detailsCount = MainLogic.Inst.leftSpace.GetCountParts() + MainLogic.Inst.rightSpace.GetCountParts();
            CreateArray();
            float step = 100;
            float x = leftDownPoint.localPosition.x;
            float y = _currentYforDetail;
            for (int i = 0; i < _detailsCount; i++) {
                Detail det = GetRandomDetail();

                int k = 0;
                while (true) {
                    Vector3 pos = new Vector3(x, y, 0);
                    if (CheckDisForOtherDetailOnConveyor(pos, det) && x - det.radius > leftDownPoint.localPosition.x && x + det.radius < rightUpPoint.localPosition.x) {
                        det.detailOnConveyor = true;

                        det.transform.SetParent(parentForDetails); //перекидуем в общий парент деталей
                        det.SetRobotPosition(); //запоминаем стартовую позицию на роботе

                        det.SetDetailAngle(); //берём рандомный угол

                        det.SetParentConveyor(parentForMoveDetails); //перекидуем в парент движущихся деталей
                        det.SetConveyorPosition(new Vector3(x, y, 0));

                        det.gameObject.SetActive(true);
                        break;
                    }

                    x += step;
                    if (x > rightUpPoint.localPosition.x) {
                        x = leftDownPoint.localPosition.x;
                        y += step;
                    }

                    k++;
                    if (k > 10000) {
                        Debug.Log("BAD");
                        break;
                    }
                }
            }
        }


        public bool CheckDisForOtherDetailOnConveyor(Vector3 pos, Detail detail) {
            bool isGood = true;
            for (int i = 0; i < randomMassiv.Count; i++) {
                if (randomMassiv[i].index != detail.index && randomMassiv[i].detailOnConveyor) {
                    float currentDis = Vector2.Distance(pos, randomMassiv[i].conveyerPos);
                    float dis = detail.radius + randomMassiv[i].radius;
                    if (currentDis < dis) {
                        isGood = false;
                        break;
                    }
                }
            }

            return isGood;
        }

        private void CreateArray() {
            int i = 0;
            foreach (Detail e in MainLogic.Inst.leftSpace.robot.partsList) {
                randomMassiv.Add(e);
                indexArray.Add(i);
                i++;
            }

            foreach (Detail e in MainLogic.Inst.rightSpace.robot.partsList) {
                randomMassiv.Add(e);
                indexArray.Add(i);
                i++;
            }
        }

        private void Update() {
            if (canMoveConveyor && !haveCollision) {
                _movingTape.localPosition += Vector3.down * SOController.Inst.rs.conveyorSpeed * Time.deltaTime;
                AudioManager.Inst.PlayConveyorSound(true);
            } else {
                AudioManager.Inst.PlayConveyorSound(false);
            }
        }


        private Detail GetRandomDetail() {
            int r = Random.Range(0, indexArray.Count);
            int iR = indexArray[r];
            indexArray.RemoveAt(r);

            return randomMassiv[iR];
        }

        //расстановка деталей по центру конвеера
        /*  private void PlaceDetailOnConveyor(Detail det) {
              det.transform.SetParent(parentForDetails);//перекидуем в общий парент деталей
              det.SetRobotPosition();//запоминаем стартовую позицию на роботе
              det.SetDetailAngle();//берём рандомный угол
              det.SetParentConveyor(parentForMoveDetails);//перекидуем в парент движущихся деталей
              _currentYforDetail += det.radius;//увеличиваем позицию
              det.SetConveyorPosition(new Vector3(0, _currentYforDetail, 0));
              _currentYforDetail += det.radius;//увеличиваем позицию для следующей детали
              det.gameObject.SetActive(true);
          }*/
    }
}