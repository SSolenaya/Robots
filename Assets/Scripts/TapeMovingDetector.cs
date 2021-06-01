using System;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.Robots {
    public class TapeMovingDetector : MonoBehaviour {
        [SerializeField] [ReadOnly] private bool _haveCollision;

        [ReadOnly] public List<Detail> listDetails;

        public bool haveCollision {
            get => _haveCollision;
            set {
                _haveCollision = value;
                if (actionHaveCollision != null) {
                    actionHaveCollision(_haveCollision);
                }
            }
        }

        public Action<bool> actionHaveCollision;


        public void AddParts(Detail det) {
            bool needAdd = true;
            foreach (Detail d in listDetails) {
                if (det.index == d.index) {
                    needAdd = false;
                    break;
                }
            }

            if (needAdd) {
                listDetails.Add(det);
            }

            actionHaveCollision(listDetails.Count > 0);
        }

        public void RemoveParts(Detail det) {
            for (int i = 0; i < listDetails.Count; i++) {
                if (det.index == listDetails[i].index) {
                    listDetails.RemoveAt(i);
                    break;
                }
            }

            actionHaveCollision(listDetails.Count > 0);
        }

        private void OnTriggerEnter2D(Collider2D collider2D) {
            Detail det = collider2D.GetComponent<Detail>();
            if (det != null) {
                AddParts(det);
            }
        }

        private void OnTriggerExit2D(Collider2D collider2D) {
            Detail det = collider2D.GetComponent<Detail>();
            if (det != null) {
                RemoveParts(det);
            }
        }
    }
}