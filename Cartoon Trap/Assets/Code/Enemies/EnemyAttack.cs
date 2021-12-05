using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float resetTimer = 0.5f;
    public float timeElapsed = 0f;
    public float attackOn = 0.25f;
    public int damage = 1;
    public float range = 0.05f;

    private bool onRange = false;
    private bool attackDone = false;

    private PlayerController player;
    private CircleCollider2D attackCollider;

    private void Awake()
    {
        attackCollider = GetComponent<CircleCollider2D>();
        attackCollider.radius = range;
    }

    private void FixedUpdate()
    {
        if(timeElapsed == 0)
        {
            if (onRange)
            {
                StartAttack();
            }
        }
        else
        {
            if (timeElapsed < resetTimer)
            {
                if(timeElapsed >= attackOn && !attackDone)
                {
                    attackDone = true;
                    DoAttack();
                }
                timeElapsed += Time.fixedDeltaTime;
            }
            else
            {
                FinishAttack();
            }
        }
    }

    private void StartAttack()
    {
        timeElapsed += Time.fixedDeltaTime;
    }

    private void DoAttack()
    {
        if (onRange)
        {
            player.initialCurrentHealth -= damage;
        }
    }

    private void FinishAttack()
    {
        timeElapsed = 0;
        attackDone = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onRange = true;
            player = GameObject.FindObjectOfType<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = null;
            onRange = false;
        }
    }
}
