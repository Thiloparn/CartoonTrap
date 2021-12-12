using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeEffectAreaCollider : EffectAreaCollider
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(damage);
        }
    }
}
