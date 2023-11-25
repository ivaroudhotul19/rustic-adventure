using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FishAI : MonoBehaviour
{
    public float jumpSpeed;             
    Rigidbody2D rb;
    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        FishJump();
    }

    void Update()
    {
        if (rb.velocity.y > 0)
        {
            sr.flipY = false;   // facing up
        }
        else
        {
            sr.flipY = true;
        }
    }

    public void FishJump()
    {
        rb.AddForce(new Vector2(0, jumpSpeed));
    }
}
