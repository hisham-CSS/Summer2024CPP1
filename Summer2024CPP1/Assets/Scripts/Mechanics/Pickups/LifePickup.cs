using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePickup : MonoBehaviour, IPickup
{
    Rigidbody2D rb;
    GroundCheck gndChk;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gndChk = GetComponent<GroundCheck>();
        rb.AddForce(Vector3.left * 3, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gndChk.IsGrounded()) return;

        rb.velocity = Vector3.left * 3;
    }

    public void Pickup(GameObject player)
    {
        Debug.Log("this object is picked up");
        Destroy(gameObject);
        //Destroy logic
    }
}
