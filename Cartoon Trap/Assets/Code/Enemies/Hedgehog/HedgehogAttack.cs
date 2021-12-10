using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogAttack : MonoBehaviour
{
    public float resetTimer = 0.5f;
    public float timeElapsed = 0f;
    public float attackOn = 0.25f;

    public float projectilRange = 5f;
    public int projectilDamage = 1;
    public float projectilSpeed = 4f;

    private bool onRange = false;
    private bool attackDone = false;

    private PlayerController player;
    private CircleCollider2D attackCollider;
    [SerializeField] Hedgehog hedgehog;
    [SerializeField] Projectil projectil;

    private void Awake()
    {
        attackCollider = GetComponent<CircleCollider2D>();
    }

    private void FixedUpdate()
    {
        if (timeElapsed == 0 && (hedgehog.attackPlayer || onRange))
        {
            StartAttack();
        }
        else if (timeElapsed > 0 && (hedgehog.attackPlayer || onRange))
        {
            if (timeElapsed < resetTimer)
            {
                if (timeElapsed >= attackOn && !attackDone)
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
        Projectil proj;

        if(transform.localScale.x == -1f)
        {
            proj = Instantiate(projectil, transform.position, Quaternion.identity);
        }
        else
        {
            proj = Instantiate(projectil, transform.position, Quaternion.Inverse(Quaternion.identity));
        }

        proj.createProjectile(new Vector2(-transform.localScale.x, 0), projectilSpeed, projectilRange, projectilDamage);
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
