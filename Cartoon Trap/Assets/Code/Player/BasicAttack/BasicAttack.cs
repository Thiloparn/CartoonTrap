using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public PlayerController player;
    private IAction attack;

    private Collider2D attackCollider;
    private const float ATTACK_COLLIDER_OFFSET = 0.055f;

    public float attackDuration = 0.5f;
    public float attackElapsed = 0f;

    private bool touchingOno = false;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
        attackCollider.enabled = false;
        attack = new Attack(attackCollider.transform);
    }

    private void FixedUpdate()
    {
        SetColliderOffset();

        if (player.Attacking)
        {

            if (attackElapsed == 0)
            {
                StartAttack();
            }
            else if (attackElapsed < attackDuration)
            {
                attackElapsed += Time.fixedDeltaTime;
            }
            else
            {
                FinishAttack();
            }

        }
    }

    private void SetColliderOffset()
    {
        Vector2 newOffset = new Vector2(0, 0);

        if (player.MovingDirectionY != 0)
        {
            if (player.MovingDirectionY < 0)
            {
                newOffset.y -= 1;
            }
            else
            {
                newOffset.y += 1;
            }
        }
        else
        {
            if (player.MovingDirectionX < 0)
            {
                newOffset.x -= 1;
            }
            else
            {
                newOffset.x += 1;
            }
        }

        attackCollider.offset = newOffset * ATTACK_COLLIDER_OFFSET;
    }

    private void FinishAttack()
    {
        attackCollider.enabled = false;
        attackElapsed = 0;
        player.Attacking = false;
        if (touchingOno == false)
        {
            attack.ExecuteAction(player);
        }
    }

    private void StartAttack()
    {
        touchingOno = false;
        attackCollider.enabled = true;
        attackElapsed += Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Onomatopeya")
        {
            touchingOno = true;
        }
    }
}
