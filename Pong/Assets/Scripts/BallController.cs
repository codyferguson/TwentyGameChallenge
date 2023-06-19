using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] float speed = 100f;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] int resetWaitSeconds = 1;
    

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
        StartCoroutine(ResetBall());
    }

    public IEnumerator ResetBall() {
        rigidBody.position = Vector3.zero;
        rigidBody.velocity = Vector3.zero;

        yield return new WaitForSeconds(resetWaitSeconds);

        // TODO: Set ball to be served by server instead
        AddStartingForce();
    }

    public void Update() {
        if (Input.GetMouseButtonDown(0)) {
            inMotion = true;
            //movement = Camera.main.ScreenToWorldPoint(Input.mousePosition); -- use for launching ball
        }
    }

    public void AddForce(Vector2 force) {
        rigidBody.AddForce(force);
    }

    private void AddStartingForce() {
        // Random to go left or right
        direction.x = Random.value < 0.5f ? -1.0f : 1.0f;
        // Avoid 0 value so as to not travel Horizontal
        direction.y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) :
                                        Random.Range(0.5f, 1.0f);

        rigidBody.AddForce(direction * speed);
    }
}
