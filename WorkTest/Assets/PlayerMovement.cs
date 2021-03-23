using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform groundCheck;

    public bool onGround;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    void checkGround()
    {
        onGround = Physics2D.OverlapCircle();
    }
}
