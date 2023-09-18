using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScoringZone : MonoBehaviour
{
    public AudioClip sound;
    UnityEvent scoreEvent;

    public void Start() {
        GameManager gameManager = GameManager.instance;
        SoundManager soundManager = SoundManager.instance;

        if (scoreEvent == null) {
            scoreEvent = new UnityEvent();
        }

        // I don't like the hardcoded name but this is better than using Unity UI
        scoreEvent.AddListener(gameObject.name == "Left Boundary" ?  gameManager.PlayerTwoScores : gameManager.PlayerOneScores);
        scoreEvent.AddListener(() => soundManager.PlaySingle(sound));
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        BallController ball = collision.gameObject.GetComponent<BallController>();

        if (ball != null) {
            scoreEvent.Invoke();
        }
    }
}
