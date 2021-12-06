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
    private float attackElapsed = 0f;
    private const int MAX_ATTACK_COMBO = 3;
    public int numAttacksInCurrentCombo = 0;
    public float maxTimeBetweenAttacks = 0.2f;
    public float timeElapsedBetweenAttacks = 0f;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
        attackCollider.enabled = false;
        attack = new Attack();
    }

    private void FixedUpdate()
    {
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
        else
        {
            SetColliderOffset();
            timeElapsedBetweenAttacks += Time.deltaTime;
            if (timeElapsedBetweenAttacks >= maxTimeBetweenAttacks)
            {
                numAttacksInCurrentCombo = 0;
                timeElapsedBetweenAttacks = 0;
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
        if (numAttacksInCurrentCombo == MAX_ATTACK_COMBO)
        {
            print("3 ataque");
            attack.ExecuteAction(player);
            numAttacksInCurrentCombo = 0;
        }

        player.playerAnimator.EndPunchingAnimation(player);
    }

    private void StartAttack()
    {
        attackCollider.enabled = true;
        attackElapsed += Time.fixedDeltaTime;
        ++numAttacksInCurrentCombo;
        timeElapsedBetweenAttacks = 0;
        player.playerAnimator.StartPunchingAnimation(player);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(player.MovingDirectionY < 0 && collision.gameObject.tag == "Enemy" && player.attacking)
        {
            player.rebound.ExecuteAction(player);
        }
    }
}
