using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;

public class Bird : MonoBehaviour
{
    private const float JUMP_AMOUNT = 100f;
    private static Bird instance;

    private State state;
    public static Bird GetInstanse()
    {
        return instance;
    }
    public event EventHandler onDied;
    public event EventHandler OnStartedPlaying;
    Rigidbody2D rb;

    private enum State
    {
        WaitingToStart,
        Playing,
        Dead,
    }
    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.WaitingToStart:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    state = State.Playing;
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    Jump();
                    if (OnStartedPlaying != null) OnStartedPlaying(this, EventArgs.Empty);
                }
                break;
            case State.Playing:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    Jump();
                }
                break;
            case State.Dead:
                break;
        }
        
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * JUMP_AMOUNT;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {      
        rb.bodyType = RigidbodyType2D.Static;
        if (onDied != null) onDied(this, EventArgs.Empty);
    }
}
