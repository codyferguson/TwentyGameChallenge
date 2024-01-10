using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float speed = 100f;
    [SerializeField] int resetWaitSeconds = 1;
    [SerializeField] Vector2 startingPosition;
    [SerializeField] float sqrMaxVelocity = 30f;


    Rigidbody2D rigidBody;

    Vector2 direction;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start() {
        StartCoroutine(ResetBall());
    }

    public IEnumerator ResetBall() {
        rigidBody.position = startingPosition;
        rigidBody.velocity = Vector3.zero;

        yield return new WaitForSeconds(resetWaitSeconds);

        // TODO: Set ball to be served by server instead
        AddStartingForce();
    }

    public void AddForce(Vector2 force) {
        if (rigidBody.velocity.sqrMagnitude < sqrMaxVelocity) rigidBody.AddForce(force);
        
        //Debug.Log($"force is now {rigidBody.velocity.sqrMagnitude}");

    }

    private void AddStartingForce() {
        //direction.x = Random.value < 0.5f ? -1.0f : 1.0f;
        direction.x = 0f;
        // needs to head up from player position
        direction.y = -1f;

        rigidBody.AddForce(direction * speed);
    }
}
