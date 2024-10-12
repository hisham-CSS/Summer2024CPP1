using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Jump : MonoBehaviour
{
    public AudioClip jumpClip;
    public float jumpHeight = 5;
    public float jumpFallForce = 50;

    Rigidbody2D rb;
    PlayerController pc;

    float timeHeld;
    float maxHoldTime = 0.5f;
    float calculatedJumpForce;

    float jumpBufferTime = 0.3f;
    float currentJumpBuffer = 0;
    bool jumpCancelled = false;
    bool jumpBuffered = false;

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
     
        if (Input.GetButtonDown("Jump"))
        {
            currentJumpBuffer = Time.time;
            jumpBuffered = true;
        } 
            
        if (Input.GetButton("Jump")) timeHeld += Time.deltaTime;
       
        if (pc.IsGrounded && jumpBuffered)
        {
            jumpBuffered = false;
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, calculatedJumpForce), ForceMode2D.Impulse);
            pc.audioSource.PlayOneShot(jumpClip);
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
        
        if (jumpBuffered)
        {
            if (currentJumpBuffer + jumpBufferTime < Time.time)
            {
                currentJumpBuffer = 0;
                jumpBuffered = false;
            }
        }
    }
}
