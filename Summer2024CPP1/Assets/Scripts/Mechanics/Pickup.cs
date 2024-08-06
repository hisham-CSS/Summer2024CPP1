using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerController pc = collider.GetComponent<PlayerController>();

            switch (type)
            {
                case PickupType.Life:
                    pc.lives++;
                    break;
                case PickupType.PowerupJump:
                case PickupType.PowerupSpeed:
                    pc.PowerupValueChange(type);
                    break;
                case PickupType.Score:
                    break;
            }
            Destroy(gameObject);
        }
            
    }
}
