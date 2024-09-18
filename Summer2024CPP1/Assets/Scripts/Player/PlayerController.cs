using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(AudioSource), typeof(GroundCheck))]
public class PlayerController : MonoBehaviour
{
    //Player gameplay variables
    private Coroutine jumpForceChange;
    private Coroutine speedChange;

    public void PowerupValueChange(Pickup.PickupType type)
    {
        if (type == Pickup.PickupType.PowerupSpeed)
            StartPowerupCoroutine(ref speedChange, ref speed, type);

        if (type == Pickup.PickupType.PowerupJump)
            StartPowerupCoroutine(ref jumpForceChange, ref jumpForce, type);
    }

    public void StartPowerupCoroutine(ref Coroutine InCoroutine, ref float inVar, Pickup.PickupType type)
    {
        if (InCoroutine != null)
        {
            StopCoroutine(InCoroutine);
            InCoroutine = null;
            inVar /= 2;
        }

        InCoroutine = StartCoroutine(PowerupChange(type));
    }

    IEnumerator PowerupChange(Pickup.PickupType type)
    {
        //this code runs before the wait
        if (type == Pickup.PickupType.PowerupSpeed)
            speed *= 2;

        if (type == Pickup.PickupType.PowerupJump)
            jumpForce *= 2;

        Debug.Log($"Jump force value is {jumpForce}, Speed value is {speed}");
        
        yield return new WaitForSeconds(5.0f);

        if (type == Pickup.PickupType.PowerupSpeed)
        {
            speed /= 2;
            speedChange = null;
        }
        if (type == Pickup.PickupType.PowerupJump)
        {
            jumpForce /= 2;
            jumpForceChange = null;
        }

        Debug.Log($"Jump force value is {jumpForce}, Speed value is {speed}");
    }

    //Movement Variables
    [SerializeField, Range(1, 20)] private float speed = 5;
    [SerializeField, Range(1, 20)] private float jumpForce = 10;
    

    //GroundCheck Stuff
    private bool isGrounded = false;
    public bool IsGrounded => isGrounded;

    //Component References
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    public AudioSource audioSource;
    GroundCheck gndChk;

    //Audio Clip references
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip stompClip;

    // Start is called before the first frame update
    void Start()
    {
        //Component References Filled
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gndChk = GetComponent<GroundCheck>();

        audioSource.outputAudioMixerGroup = GameManager.Instance.SFXGroup;

        //Checking values to ensure non garbage data
        if (speed <= 0)
        {
            speed = 5;
            Debug.Log("Speed was set incorrectly");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0) return;


        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        //grab horizontal axis - Check Project Settings > Input Manager to see the inputs defined
        float hInput = Input.GetAxis("Horizontal");

        //Create a small overlap collider to check if we are touching the ground
        CheckIsGrounded();


        //Animation check for our physics
        if (curPlayingClips.Length > 0)
        {
            if (curPlayingClips[0].clip.name == "Attack")
            {
                if (isGrounded)
                    rb.velocity = Vector2.zero;
            }
            else
            {
                if (hInput != 0 && Mathf.Abs(rb.velocity.x) < 4)
                {
                    rb.AddForce(new Vector2(hInput * 10, 0));
                }
            }
        }


        //Button Input Checks
        if (Input.GetButtonDown("Fire1"))
        {
            if (!isGrounded && curPlayingClips[0].clip.name != "JumpAttack")
            {
                anim.SetTrigger("JumpAttack");
            }
            else if (!curPlayingClips[0].clip.name.Contains("Attack"))
            {
                anim.SetTrigger("Fire");
            }
        }

        //Sprite Flipping
        if (hInput != 0) sr.flipX = (hInput < 0);

        //Animation setup
        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);
    }
    
    /// <summary>
    /// This function is used to check if we are grounded.  When we jump - we disable checking if we are grounded until our velocity reaches negative on the y-axis - this indicates that we are falling and we should start to check if we are grounded again. This is done to prevent us flipping to grounded when we jump through a platform.
    /// </summary>
    void CheckIsGrounded()
    {
        if (!isGrounded)
        {
            if (rb.velocity.y <= 0)
            {
                isGrounded = gndChk.IsGrounded();
            }
        }
        else
            isGrounded = gndChk.IsGrounded();
    }

    void IncreaseGravity()
    {
        rb.AddForce(Vector2.down * 100);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.lives--;
        }

        IPickup pickup = collision.gameObject.GetComponent<IPickup>();
        if (pickup != null)
        {
            pickup.Pickup(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Squish"))
        {
            collision.gameObject.GetComponentInParent<Enemy>().TakeDamage(9999);
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            audioSource.PlayOneShot(stompClip);
        }

        IPickup pickup = collision.gameObject.GetComponent<IPickup>();
        if (pickup != null)
        {
            pickup.Pickup(gameObject);
        }
    }
}
