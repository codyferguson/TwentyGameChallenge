using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] float speed = 100f;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    

    Rigidbody2D rigidBody;
    Vector2 direction;
    bool inMotion;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Used later for serving the ball
        inMotion = false;
        ResetBall();
    }

    private void AddStartingForce() {
        // Random to go left or right
        direction.x = Random.value < 0.5f ? -1.0f : 1.0f;
        // Avoid 0 value so as to not travel Horizontal
        direction.y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) :
                                        Random.Range(0.5f, 1.0f);

        print($"X: {direction.x} Y: {direction.y}");
        rigidBody.AddForce(direction * speed);
    }

    public void ResetBall() {
        rigidBody.position = Vector3.zero;
        rigidBody.velocity = Vector3.zero;

        // TODO: Set ball to be served by server instead
        AddStartingForce();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            inMotion = true;
            //movement = Camera.main.ScreenToWorldPoint(Input.mousePosition); -- use for launching ball
        }
    }
}
