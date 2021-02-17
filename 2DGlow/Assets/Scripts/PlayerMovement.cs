 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator anim;

    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    bool jump = false;

    float animSpeed;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal")*runSpeed;

        if (horizontalMove < 0)
        {
            animSpeed = -horizontalMove;
        }
        else
        {
            animSpeed = horizontalMove;
        }

        anim.SetFloat("speed", animSpeed);

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove*Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
