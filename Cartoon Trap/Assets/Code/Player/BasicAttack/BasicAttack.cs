using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public PlayerController player;
    private IAction attack;
    private Grap grap;
    private IAction slash = new Slash();
    private IAction pum = new Pum();

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

    //Slash
    public float slashDuration = 0.5f;
    private float slashElapsed = 0f;

    //Pum
    public float pumDuration = 0.5f;
    private float pumElapsed = 0f;

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
            }else if (player.UsingBlade)
            {
                PerformSlash();
            }else if (player.UsingHammer)
            {
                PerformPum();
            }
            else
            {
                SetColliderOffset();
            }

            ManageCombos();            
        }
    }

    private void PerformPum()
    {
        if (pumElapsed == 0)
        {
            StartPum();
        }
        else if (pumElapsed < pumDuration)
        {
            pumElapsed += Time.fixedDeltaTime;
        }
        else
        {
            FinishPum();
        }
    }

    private void FinishPum()
    {
        attackCollider.enabled = false;
        player.UsingHammer = false;
        pum.ExecuteAction(player);
        gameObject.tag = "Untagged";
        pumElapsed = 0;
    }

    private void StartPum()
    {
        attackCollider.enabled = true;
        pumElapsed += Time.fixedDeltaTime;
        player.playerAnimator.StartUsingHammerAnimation(player);
        player.PlayPlayerAudio(player.pumSound);
        gameObject.tag = "Hammer";
    }

    private void PerformSlash()
    {
        if (slashElapsed == 0)
        {
            StartSlash();
        }
        else if (slashElapsed < slashDuration)
        {
            slashElapsed += Time.fixedDeltaTime;
        }
        else
        {
            FinishSlash();
        }
    }

    private void FinishSlash()
    {
        attackCollider.enabled = false;
        player.UsingBlade = false;
        slash.ExecuteAction(player);
        gameObject.tag = "Untagged";
        slashElapsed = 0;
    }

    private void StartSlash()
    {
        attackCollider.enabled = true;
        slashElapsed += Time.fixedDeltaTime;
        player.playerAnimator.StartUsingBladeAnimation(player);
        player.PlayPlayerAudio(player.slashSound);
        gameObject.tag = "Blade";
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
        player.playerAnimator.StartGrappingAnimation(player);
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
        player.playerAnimator.StartGrappingAnimation(player);
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

        if (player.MovingDirectionY > 0)
        {
            newOffset.y += 1;
        }
        else
        {
            if (player.isGrounded())
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
            else
            {
                if (player.MovingDirectionY < 0)
                {
                    newOffset.y -= 1;
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
        gameObject.tag = "Punch";

        AnimatePunch();
        player.PlayPlayerAudio(player.punchSound);

    }

    private void AnimatePunch()
    {
        if (player.MovingDirectionY > 0)
        {
            player.playerAnimator.StartPunchingUpAnimation(player);
        }
        else
        {
            if (player.isGrounded())
            {
                player.playerAnimator.StartPunchingAnimation(player);
            }
            else
            {
                if (player.MovingDirectionY < 0)
                {
                    player.playerAnimator.StartPunchingDownAnimation(player);
                }
                else
                {
                    player.playerAnimator.StartPunchingAnimation(player);
                }
            }
        }
    }

    private void FinishAttack()
    {
        attackElapsed = 0;
        player.Attacking = false;
        if (numAttacksInCurrentCombo == MAX_ATTACK_COMBO && player.punchLocked == false)
        {
            attack.ExecuteAction(player);
            numAttacksInCurrentCombo = 0;
        }
        attackCollider.enabled = false;
        gameObject.tag = "Untagged";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;

        if(collidedObject.tag == "Enemy")
        {
            if (player.MovingDirectionY < 0 && player.Attacking)
            {
                player.rebound.ExecuteAction(player);
            }

            BasicEnemy enemy = collidedObject.GetComponent<BasicEnemy>();
            MakeDamage(enemy);

        }
        else if (player.Grapping && collidedObject.tag == "Onomatopeya" && player.Grapping)
        {
            grap.SetGrappedOno(collidedObject);
            grap.ExecuteAction(player);
        }
    }

    private void MakeDamage(BasicEnemy enemy)
    {
        if (gameObject.tag == "Punch")
        {
            enemy.TakeDamage(player.attackPower, player.gameObject);
        }
        else if (gameObject.tag == "Blade")
        {
            enemy.TakeDamage(player.bladePower, player.gameObject);
        }else if (gameObject.tag == "Hammer")
        {
            enemy.TakeDamage(player.hammerPower, player.gameObject);
        }
    }
}
