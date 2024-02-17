using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Ball : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] int resetWaitSeconds = 1;
    [SerializeField] float sqrMaxVelocity = 20f;
    [SerializeField] GameObject Paddle;

    [SerializeField] Rigidbody2D rigidBody;

    Vector2 direction;
    private bool isLaunched = false;
    public bool followingPaddle = true;
    private int lifes = 3;
    private float ySpot;
    private Vector2 velocityBeforePause = new Vector2(0,0);

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        Paddle = GameObject.Find("Paddle");
        ySpot = GameObject.Find("BallSpawnLocation").transform.position.y;
    }

    private void FixedUpdate() {
        if (followingPaddle) {
            Paddle ??= GameObject.Find("Paddle");
            Vector3 followPos = new Vector3(Paddle.transform.position.x, ySpot);

            var step = 100 * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, followPos, step);
            
        } else {
            if (rigidBody.velocity.sqrMagnitude < sqrMaxVelocity) {
                Debug.Log($"Velocity is {rigidBody.velocity.magnitude} and max is {sqrMaxVelocity}");
                rigidBody.velocity = rigidBody.velocity * speed;
            }
        }
    }

    public IEnumerator ResetBall() {
        followingPaddle = true;
        PaddleController paddleController = Paddle.GetComponent<PaddleController>();
        rigidBody.position = paddleController.GetBallSpawnLocation();
        
        rigidBody.velocity = Vector3.zero;

        // Start blink animation maybe
        yield return new WaitForSeconds(resetWaitSeconds);
        isLaunched = false;
    }

    public void AddForce(Vector2 force) {
        if (rigidBody.velocity.sqrMagnitude < sqrMaxVelocity) {
            Debug.Log("Adding force from ball script AddForce");
            rigidBody.AddForce(force);
        }
    }

    public void LaunchBall() {
        if (lifes > 0 && !isLaunched) {
            isLaunched = true;
            followingPaddle = false;
            AddStartingForce();
            //rigidBody.gravityScale = 0.1f;
        }
    }

    public int DecrementLife() {
        // want to return "before" decrementing here
        return lifes--;
    }

    public void PauseBall() {
        velocityBeforePause = rigidBody.velocity;
        rigidBody.velocity = new Vector2(0, 0);
    }

    public void UnpauseBall() {
        rigidBody.velocity = velocityBeforePause;
    }

    private void AddStartingForce() {
        //direction.x = Random.value < 0.5f ? -1.0f : 1.0f;
        direction.x = 0f;
        // needs to head up from player position
        direction.y = 1f;
        Debug.Log("Adding force from ball script AddStartingForce");
        rigidBody.AddForce(direction * speed);
    }
}
