using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttack : MonoBehaviour
{
    public float resetTimer = 0;
    private float animationLenght;
    private float timeElapsed = 0;
    public int attackOnFrame = 1;
    public float attackOn = 0;
    public int damage = 1;

    private bool onRange = false;
    private bool attackDone = false;

    private PlayerController player;
    [SerializeField] Soldier soldier;
    [SerializeField] Animator anim;

    private void Awake()
    {
        attackOn = attackOnFrame / 60f;
    }

    private void FixedUpdate()
    {
        if (timeElapsed == 0 && soldier.attackPlayer)
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
                if (timeElapsed < animationLenght)
                {
                    if (timeElapsed >= attackOn && !attackDone)
                    {
                        attackDone = true;
                        DoAttack();
                    }
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
        anim.SetTrigger("Attack");
        animationLenght = anim.GetCurrentAnimatorStateInfo(0).length;
        timeElapsed += Time.fixedDeltaTime;
    }

    private void DoAttack()
    {
        if (onRange)
        {
            player.TakeDamage(damage);
            player.playerAnimator.StartHurtingAnimation(player);
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
            onRange = false;
            player = null;
        }
    }
}
