using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMvmt : MonoBehaviour {

    [SerializeField]
#pragma warning disable CS0649 // Field 'PlayerMvmt.speed' is never assigned to, and will always have its default value 0
    private float speed;
#pragma warning restore CS0649 // Field 'PlayerMvmt.speed' is never assigned to, and will always have its default value 0

    private Vector2 direction;

    void Start() {

    }

    void FixedUpdate() {
        GetInput();
        //direction = Vector2.right;
        Move();
    }

    public void Move()
    {
        transform.Translate(direction * speed);
    }

    private void GetInput()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
    }
}
