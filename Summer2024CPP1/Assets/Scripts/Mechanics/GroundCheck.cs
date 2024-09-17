using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField, Range(0.01f, 1)] private float groundCheckRadius = 0.02f;
    [SerializeField] private LayerMask isGroundLayer;

    // Start is called before the first frame update
    void Start()
    {
        //Creating groundcheck object
        if (!groundCheck)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(transform);
            obj.transform.localPosition = Vector3.zero;
            obj.name = "GroundCheck";
            groundCheck = obj.transform;
        }
    }

    public bool IsGrounded()
    {
        if (!groundCheck) return false;
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
    }
}
