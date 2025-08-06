using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowlmovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 1f;
    Vector2 movement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
