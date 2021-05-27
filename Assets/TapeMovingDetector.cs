using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TapeMovingDetector : MonoBehaviour {
    public UnityEvent stopEvent;
    public UnityEvent moveEvent;

    void OnTriggerEnter2D(Collider2D collider2D) {
        Debug.Log(collider2D.name);
        stopEvent.Invoke();
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        Debug.Log("No colliders");
       // moveEvent.Invoke();
    }
}
