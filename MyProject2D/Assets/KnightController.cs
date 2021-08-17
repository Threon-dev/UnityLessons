using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    private Rigidbody2D rb;

    private const float MOVE_SPEED = 10f;
    private const float JUMP_AMOUNT = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * JUMP_AMOUNT, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity += Vector2.left * MOVE_SPEED * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {

        }
    }

    private void FixedUpdate()
    {
       
    }
}
