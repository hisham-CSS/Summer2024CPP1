using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpHeight = 5;
    public float jumpFallForce = 50;

    Rigidbody2D rb;
    PlayerController pc;

    float timeStarted;
    float timeHeld;
    float maxHoldTime = 0.5f;
    float calculatedJumpForce;

    bool jumpCancelled = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
        calculatedJumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.IsGrounded) jumpCancelled = false;
     
        if (Input.GetButtonDown("Jump") && pc.IsGrounded)
        {
            timeHeld += Time.deltaTime;
            rb.AddForce(new Vector2(0, calculatedJumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetButtonUp("Jump") || maxHoldTime < timeHeld)
        {
            timeHeld = 0;
            jumpCancelled = true;
        }

        if (jumpCancelled)
        {
            if (rb.velocity.y < -10) return;
            rb.AddForce(Vector2.down * jumpFallForce);
        }
        
    }
}
