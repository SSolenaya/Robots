using UnityEngine;

namespace EnglishKids.Robots {
    [CreateAssetMenu(fileName = "RobotsSettings", menuName = "ScriptableObjects/RobotsSettings")]
    public class RobotsSettings : ScriptableObject {
        [Header("Скорость конвеера")] public float conveyorSpeed = 100;

        [Header("Минимальная дистанция до необходимой позиции")]
        public float minDetailDis = 30;

        [Header("Коэф для увеличения области клика на детали")]
        public float deltaCollider = 0.15f;

        [Header("Скейл роботов")] public float scaleRobots = 0.8f;
    }
}