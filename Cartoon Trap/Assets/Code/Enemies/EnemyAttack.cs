using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackDuration = 0.5f;
    public float attackElapsed = 0f;
    public int damage = 1;

    private bool attacking = false;

    private PlayerController player;

    private void FixedUpdate()
    {
        if(attackElapsed == 0)
        {
            if (attacking)
            {
                StartAttack();
            }
        }
        else
        {
            if (attackElapsed < attackDuration)
            {
                attackElapsed += Time.fixedDeltaTime;
            }
            else
            {
                FinishAttack();
            }
        }
    }

    private void StartAttack()
    {
        attackElapsed += Time.fixedDeltaTime;
        player.initialCurrentHealth -= damage;
    }

    private void FinishAttack()
    {
        attackElapsed = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attacking = true;
            player = GameObject.FindObjectOfType<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = null;
            attacking = false;
        }
    }
}
