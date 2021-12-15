using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogAttack : MonoBehaviour
{
    public float resetTimer = 0.5f;
    public float timeElapsed = 0f;
    private float attackOn = 0.25f;
    public int attackOnFrame = 1;

    public float projectilRange = 5f;
    public int projectilDamage = 1;
    public float projectilSpeed = 4f;

    private bool onRange = false;
    private bool attackDone = false;

    private PlayerController player;
    private CircleCollider2D attackCollider;
    [SerializeField] Hedgehog hedgehog;
    [SerializeField] Projectil projectil;
    [SerializeField] Animator anim;

    private void Awake()
    {
        attackCollider = GetComponent<CircleCollider2D>();
        attackOn = attackOnFrame / 60f;
    }

    private void FixedUpdate()
    {
        if (timeElapsed == 0 && (hedgehog.attackPlayer || onRange))
        {
            StartAttack();
        }
        else if (timeElapsed > 0)
        {
            timeElapsed += Time.fixedDeltaTime;
            if (timeElapsed < resetTimer && (hedgehog.attackPlayer || onRange))
            {
                if (timeElapsed >= attackOn && !attackDone)
                {
                    attackDone = true;
                    DoAttack();
                }
            }
            else if (timeElapsed > resetTimer)
            {
                FinishAttack();
            }
        }
    }

    private void StartAttack()
    {
        anim.SetBool("Defend", false);
        anim.SetTrigger("Attack");
        timeElapsed += Time.fixedDeltaTime;
    }

    private void DoAttack()
    {
        Projectil proj;
        float direction;

        if(hedgehog.transform.localScale.x < 0)
        {
            proj = Instantiate(projectil, transform.position, Quaternion.identity);
            direction = 1f;
        }
        else
        {
            proj = Instantiate(projectil, transform.position, Quaternion.Euler(0, 0, 180));
            direction = -1f;
        }

        proj.createProjectile(new Vector2(direction, 0), projectilSpeed, projectilRange, projectilDamage, hedgehog.gameObject);
        anim.SetBool("Defend", true);
    }

    private void FinishAttack()
    {
        timeElapsed = 0;
        attackDone = false;
        hedgehog.attackPlayer = false;
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
