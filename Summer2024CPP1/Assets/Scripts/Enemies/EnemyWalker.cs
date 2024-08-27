using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyWalker : Enemy
{
    Rigidbody2D rb;
    [SerializeField] private float xVel;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        if (xVel <= 0) xVel = 3;
    }

    public override void TakeDamage(int damage)
    {
        //do something to trigger our squash animation
        if (damage == 9999)
        {
            anim.SetTrigger("Squish");
            Destroy(transform.parent.gameObject, 2);
            return;
        }

        base.TakeDamage(damage);
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        if (curPlayingClips[0].clip.name.Contains("Walk"))
        {                       
            rb.velocity = (sr.flipX) ? new Vector2(-xVel, rb.velocity.y) : new Vector2(xVel, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrier"))
        {
            anim.SetTrigger("TurnAround");
            sr.flipX = !sr.flipX;
        }
    }

}
