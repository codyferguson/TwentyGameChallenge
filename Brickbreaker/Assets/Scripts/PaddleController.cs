using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] public float speed = 10.0f;
    protected Rigidbody2D _rigidBody;
    private Vector2 _direction;

    void Awake()
    {
        if (_rigidBody == null) _rigidBody = GetComponent<Rigidbody2D>();
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
