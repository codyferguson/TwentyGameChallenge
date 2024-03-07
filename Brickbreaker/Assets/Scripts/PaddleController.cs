using UnityEngine;
using UnityEngine.Events;

public class PaddleController : MonoBehaviour
{
    public GameObject ballPrefab;

    [SerializeField] public float speed = 10.0f;
    protected Rigidbody2D _rigidBody;
    private Vector2 _direction;

    UnityEvent ballResetEvent;
    private GameObject ball;
    private Ball ballController;
    private bool isPaused = true;

    void Awake()
    {
        if (_rigidBody == null) _rigidBody = GetComponent<Rigidbody2D>();

        if (ballResetEvent == null) {
            ballResetEvent = new UnityEvent();
        }

        ball = Instantiate(ballPrefab, GetBallSpawnLocation(), Quaternion.identity);
        ballController = ball.GetComponent<Ball>();
    }

    /// <summary>
    /// This method takes in a point 2d to determine in what area of the paddle the point is.
    /// This could all be done with three box colliders probably
    /// </summary>
    /// <returns> string as either "left" "right" or "center" </returns>
    public Vector2 DeterminePaddleRegion(float xLocation) {

        float centerX = transform.position.x;
        float paddleLength = transform.localScale.x;
        
        float section = paddleLength / 6;
        float leftEdge = centerX - section;
        float rightEdge = centerX + section;
        bool hitLeftEdge = leftEdge > xLocation;
        bool hitRightEdge = rightEdge < xLocation;

        Vector2 direction = hitLeftEdge ? new Vector2(-1, 1) : hitRightEdge ? new Vector2(1, 1) : new Vector2(0, 1);
        return direction.normalized;
    }

    public Vector3 GetBallSpawnLocation() {
        Vector3 ballSpawnLocation = GameObject.Find("BallSpawnLocation").transform.localPosition;
        return transform.TransformPoint(ballSpawnLocation);
    }

    public void TogglePaddlePause(bool pausePaddle) {
        isPaused = pausePaddle;
    }

    private void Update() {
        // Get direction works for either player
        bool validLeftKeys = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool validRightKeys = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        _direction = validLeftKeys ? Vector2.left : validRightKeys ? Vector2.right : Vector2.zero;

        if (!isPaused && Input.GetKey(KeyCode.Space)) {
            ballController.LaunchBall();
        }
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        if (_direction.sqrMagnitude != 0 && !isPaused) {
            _rigidBody.AddForce(_direction * speed);
        }
    }
}
