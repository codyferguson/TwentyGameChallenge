using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] int speed = 100;

    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        this.gameObject.transform.Translate(movement * speed * Time.deltaTime);
    }
}
