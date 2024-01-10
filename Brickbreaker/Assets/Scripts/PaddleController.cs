using UnityEngine;
using UnityEngine.UIElements;

public class PaddleController : MonoBehaviour
{
    [SerializeField] public float speed = 10.0f;
    protected Rigidbody2D _rigidBody;
    private Vector2 _direction;

    void Awake()
    {
        if (_rigidBody == null) _rigidBody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// This method takes in a point 2d to determine in what area of the paddle the point is.
    /// This could all be done with three box colliders probably
    /// </summary>
    /// <returns> string as either "left" "right" or "center" </returns>
    public Vector2 DeterminePaddleRegion(float xLocation) {

        float centerX = transform.position.x;
        float paddleLength = transform.localScale.x;
        //Debug.Log($"center x pos is {centerX} and ball hit {xLocation} and scale x is {paddleLength}");
        
        float section = paddleLength / 6;
        float leftEdge = centerX - section;
        float rightEdge = centerX + section;
        //Debug.Log($"center region goes from {leftEdge} to {rightEdge} and ball hit {xLocation}");
        bool hitLeftEdge = leftEdge > xLocation;
        bool hitRightEdge = rightEdge < xLocation;
        //Debug.Log($"ball hit left? {hitLeftEdge} Ball hit right? {hitRightEdge}");

        Vector2 direction = hitLeftEdge ? new Vector2(-1, 1) : hitRightEdge ? new Vector2(1, 1) : new Vector2(0, 1);
        return direction.normalized;
    }

    private void Update() {
        // Get direction works for either player
        bool validLeftKeys = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool validRightKeys = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        _direction = validLeftKeys ? Vector2.left : validRightKeys ? Vector2.right : Vector2.zero;
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        if (_direction.sqrMagnitude != 0) {
            _rigidBody.AddForce(_direction * speed);
        }
    }
}
