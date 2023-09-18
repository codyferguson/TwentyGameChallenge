using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : Paddle
{
    public Rigidbody2D ball;

    
    public void Start() {
        Debug.Log($"Starting computer script for {name}");
    }

    public void OnEnable() {
        Debug.Log($"test this");
    }

    public void FixedUpdate() {

        // calculate the direction from the ball to the paddle using the position of each object
        Vector2 ballDirection = transform.position - ball.transform.position;
        // calculate the relative velocity between the two objects
        Vector2 relativeVelocity = ball.velocity - _rigidBody.velocity;
        // calculate the dot product of the two
        float dotProduct = Vector2.Dot(relativeVelocity, ballDirection.normalized);
        if (dotProduct > 0) {
            MovePaddle(ballDirection * -1);
        } else {
            Vector2 zeroDirection = transform.position.y > 0.0f ? Vector2.down : Vector2.up;
            MovePaddle(zeroDirection);
        }
    }

    private void MovePaddle(Vector2 direction) {
        _rigidBody.AddForce(direction * speed);
    }
}
