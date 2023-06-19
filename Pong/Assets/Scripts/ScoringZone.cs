using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScoringZone : MonoBehaviour
{
    public EventTrigger.TriggerEvent scoreTrigger; // remove this
    UnityEvent testEvent;

    public void Start() {
        GameManager gameManager = GameManager.instance;
        if (testEvent == null) {
            testEvent = new UnityEvent();
        }

        testEvent.AddListener(gameManager.PlayerTwoScores);
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        BallController ball = collision.gameObject.GetComponent<BallController>();

        if (ball != null) {
            BaseEventData eventData = new BaseEventData(EventSystem.current);
            this.scoreTrigger.Invoke(eventData);
            testEvent.Invoke();
        }
    }
}
