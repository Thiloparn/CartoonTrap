using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public PlayerController player;
    private IAction attack;
    private Grap grap;

    private Collider2D attackCollider;
    private const float ATTACK_COLLIDER_OFFSET = 0.055f;

    //Attack
    public float attackDuration = 0.5f;
    private float attackElapsed = 0f;
    private const int MAX_ATTACK_COMBO = 3;
    private int numAttacksInCurrentCombo = 0;
    public float maxTimeBetweenAttacks = 0.2f;
    private float timeElapsedBetweenAttacks = 0f;

    //Grap
    public float grapDuration = 0.5f;
    private float grapElapsed = 0f;

    //Throw
    public float throwDuration = 0.5f;
    private float throwElapsed = 0f;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
        attackCollider.enabled = false;
        attack = new Attack();
        grap = new Grap();
        gameObject.tag = "Untagged";
    }

    private void FixedUpdate()
    {
        if (player.Attacking)
        {
            PerformAttack();
        }
        else
        {
            if (player.Grapping)
            {
                PerformGrap();
            }
            else if (player.Throwing)
            {
                PerformThrow();
            }
            else
            {
                SetColliderOffset();
            }

            ManageCombos();            
        }
    }

    private void PerformThrow()
    {
        if (throwElapsed == 0)
        {
            StartThrow();
        }
        else if (throwElapsed < throwDuration)
        {
            throwElapsed += Time.fixedDeltaTime;
        }
        else
        {
            FinishThrow();
        }
    }

    private void StartThrow()
    {
        grap.ExecuteAction(player);
        throwElapsed += Time.fixedDeltaTime;
    }
    private void FinishThrow()
    {
        player.Throwing = false;
        throwElapsed = 0;
    }

    private void PerformGrap()
    {
        if (grapElapsed == 0)
        {
            StartGrap();
        }
        else if (grapElapsed < grapDuration)
        {
            grapElapsed += Time.fixedDeltaTime;
        }
        else
        {
            FinisGrap();
        }
    }

    private void StartGrap()
    {
        attackCollider.enabled = true;
        grapElapsed += Time.fixedDeltaTime;
    }
    private void FinisGrap()
    {
        attackCollider.enabled = false;
        player.Grapping = false;
        grapElapsed = 0;
    }

    private void ManageCombos()
    {
        timeElapsedBetweenAttacks += Time.deltaTime;
        if (timeElapsedBetweenAttacks >= maxTimeBetweenAttacks)
        {
            numAttacksInCurrentCombo = 0;
            timeElapsedBetweenAttacks = 0;
        }
    }

    private void PerformAttack()
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

    private void StartAttack()
    {
        attackCollider.enabled = true;
        attackElapsed += Time.fixedDeltaTime;
        ++numAttacksInCurrentCombo;
        timeElapsedBetweenAttacks = 0;
        player.playerAnimator.StartPunchingAnimation(player);
        gameObject.tag = "Punch";
    }

    private void FinishAttack()
    {
        attackElapsed = 0;
        player.Attacking = false;
        if (numAttacksInCurrentCombo == MAX_ATTACK_COMBO)
        {
            print("3 ataque");
            attack.ExecuteAction(player);
            numAttacksInCurrentCombo = 0;
        }
        attackCollider.enabled = false;
        gameObject.tag = "Untagged";

        player.playerAnimator.EndPunchingAnimation(player);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;

        if (player.MovingDirectionY < 0 && collidedObject.tag == "Enemy" && player.Attacking)
        {
            player.rebound.ExecuteAction(player);
        }else if(player.Grapping && collidedObject.tag == "Onomatopeya" && player.Grapping)
        {
            grap.SetGrappedOno(collidedObject);
            grap.ExecuteAction(player);
        }
    }
}
