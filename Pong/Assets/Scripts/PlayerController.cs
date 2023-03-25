using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] int speed = 100;

    Vector2 movement;

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        if(player.name == "Player 1") {
            if (Input.GetKey(KeyCode.W)) {
                player.transform.Translate(Vector2.up * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S)) {
                player.transform.Translate(Vector2.down* speed * Time.deltaTime);
            }
        }

        if (player.name == "Player 2") {
            if (Input.GetKey(KeyCode.UpArrow)) {
                player.transform.Translate(Vector2.up * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.DownArrow)) {
                player.transform.Translate(Vector2.down * speed * Time.deltaTime);
            }
        }
    }
}
