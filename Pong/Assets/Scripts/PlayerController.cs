using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Paddle
{
    [SerializeField] GameObject player;
    
    private Vector2 _direction;
    private KeyCode upKey;
    private KeyCode downKey;

    private void Awake() {
        // Why do I need to do this hack?
        if (_rigidBody == null) _rigidBody = GetComponent<Rigidbody2D>();
        bool isPlayerOne = player.name == "Player 1";
        upKey = isPlayerOne ? KeyCode.W : KeyCode.UpArrow;
        downKey = isPlayerOne ? KeyCode.S : KeyCode.DownArrow;
    }

    private void Update() {
        // Get direction works for either player
        if (Input.GetKey(upKey)) {
            _direction = Vector2.up;
        } else if (Input.GetKey(downKey)) {
            _direction = Vector2.down;
        } else {
            _direction = Vector2.zero;
        }
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        if(_direction.sqrMagnitude != 0) {
            _rigidBody.AddForce(_direction * speed);
        }
    }
}
