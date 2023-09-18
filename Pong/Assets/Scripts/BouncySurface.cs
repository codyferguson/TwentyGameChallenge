using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BouncySurface : MonoBehaviour
{
    public float bounceStrength;
    public AudioClip hitSound;

    UnityEvent hitEvent;

    public void Start() {
        SoundManager soundManager = SoundManager.instance;

        if (hitEvent == null) {
            hitEvent = new UnityEvent();
        }

        hitEvent.AddListener(() => soundManager.PlaySingle(hitSound));
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        BallController ball = collision.gameObject.GetComponent<BallController>();

        if (ball != null) {
            Debug.Log($"{this.name} collided with {collision.collider.name}");
            Vector2 normal = collision.GetContact(0).normal;
            ball.AddForce(-normal * this.bounceStrength);
            hitEvent.Invoke();
            
        }
    }
}
