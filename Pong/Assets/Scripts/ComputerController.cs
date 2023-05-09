using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : Paddle
{
    public Rigidbody2D ball;

//    // calculate the direction from the ball to the paddle using the position of each object
//    Vector2 ballDirection = transform.position - ball.transform.position;
//    // calculate the relative velocity between the two objects
//    Vector2 relativeVelocity = ball.velocity - rigidbody.velocity;
//    // calculate the dot product of the two
//    float dotProduct = Vector2.Dot(relativeVelocity, ballDirection.normalized);
//if(dotProduct > 0)
//{
//  // ball is moving towards paddle
//}
//else
//{
//  // ball is moving away from paddle
//}

    private void FixedUpdate() {
        // if ball moving right
        if (this.ball.velocity.x > 0.0f) {
            if (this.ball.position.y > this.transform.position.y) {
                _rigidBody.AddForce(Vector2.up * this.speed);
            } else if (this.ball.position.y < this.transform.position.y) {
                _rigidBody.AddForce(Vector2.down * this.speed);
            }
        } else {
            if (this.transform.position.y > 0.0f) {
                _rigidBody.AddForce(Vector2.down * this.speed);
            } else if (this.transform.position.y < 0.0f) {
                _rigidBody.AddForce(Vector2.up * speed);
            }
        }
    }
}
