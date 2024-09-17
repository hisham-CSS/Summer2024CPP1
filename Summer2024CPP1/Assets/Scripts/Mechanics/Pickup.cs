using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//this is fine in a small scope - but it is hard to scale.
[RequireComponent(typeof(SpriteRenderer), (typeof(AudioSource)))]
public class Pickup : MonoBehaviour
{
    public enum PickupType
    {
        Life,
        PowerupJump,
        PowerupSpeed,
        Score
    }

    [SerializeField] private PickupType type;

    public AudioClip pickupSound;

    SpriteRenderer sr;
    AudioSource audioSource;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        audioSource.outputAudioMixerGroup = GameManager.Instance.SFXGroup;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Collider2D myCollider = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(myCollider, collider);

            switch (type)
            {
                case PickupType.Life:
                    GameManager.Instance.lives++;
                    break;
                case PickupType.PowerupJump:
                case PickupType.PowerupSpeed:
                    PlayerController pc = collider.GetComponent<PlayerController>();
                    pc.PowerupValueChange(type);
                    break;
                case PickupType.Score:
                    break;
            }
            sr.enabled = false;
            audioSource.PlayOneShot(pickupSound);

            Destroy(gameObject, pickupSound.length);
        }
            
    }
}
