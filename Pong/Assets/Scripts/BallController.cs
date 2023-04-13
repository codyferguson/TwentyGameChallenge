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

    // Start is called before the first frame update
    void Start()
    {
        inMotion = false;
        ResetBall();
    }

    private void AddStartingForce() {
        // Random to go left or right
        direction.x = Random.value < 0.5f ? -1.0f : 1.0f;
        // Avoid 0 value so as to not travel Horizontal
        direction.y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) :
                                        Random.Range(0.5f, 1.0f);
        rigidBody.AddForce(direction * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Boundary")) {
            
            if (collision.gameObject.name == "Left Boundary") {
                print("player 2 score!");
                ResetBall();
                // Add score to player 2
                // Reset ball to be served player 1
                
            } else if (collision.gameObject.name == "Right Boundary") {
                print("player 1 score!");
                gameObject.SetActive(false);
                ResetBall();
                // Add score to player 1
                // Reset ball to be served by player 2
            }
        }
    }

    public void ResetBall() {
        //gameObject.SetActive(false);
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
