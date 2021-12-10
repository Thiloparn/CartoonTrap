using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAttack : MonoBehaviour
{
    public float resetTimer = 0.5f;
    public float timeElapsed = 0f;
    public float attackOn = 0.25f;

    public float projectilRange = 5f;
    public int projectilDamage = 1;
    public float projectilSpeed = 4f;

    private bool attackDone = false;

    private PlayerController player;
    private CircleCollider2D attackCollider;
    [SerializeField] Turtle turtle;
    [SerializeField] Projectil projectil;

    private void Awake()
    {
        attackCollider = GetComponent<CircleCollider2D>();
    }

    private void FixedUpdate()
    {
        if (timeElapsed == 0 && turtle.attackPlayer)
        {
            StartAttack();
        }
        else if (timeElapsed > 0 && turtle.attackPlayer)
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
        Projectil proj1 = Instantiate(projectil, transform.position, Quaternion.identity);
        proj1.createProjectile(new Vector2(transform.localScale.x, 0), projectilSpeed, projectilRange, projectilDamage);

        Projectil proj2 = Instantiate(projectil, transform.position, Quaternion.Euler(0, 0, 45));
        proj2.createProjectile(new Vector2(transform.localScale.x, Mathf.Abs(transform.localScale.x)), projectilSpeed, projectilRange, projectilDamage);

        Projectil proj3 = Instantiate(projectil, transform.position, Quaternion.Euler(0, 0, 90));
        proj3.createProjectile(new Vector2(0, Mathf.Abs(transform.localScale.x)), projectilSpeed, projectilRange, projectilDamage);

        Projectil proj4 = Instantiate(projectil, transform.position, Quaternion.Euler(0, 0, 135));
        proj4.createProjectile(new Vector2(-transform.localScale.x, Mathf.Abs(transform.localScale.x)), projectilSpeed, projectilRange, projectilDamage);

        Projectil proj5 = Instantiate(projectil, transform.position, Quaternion.Euler(0, 0, 180));
        proj5.createProjectile(new Vector2(-transform.localScale.x, 0), projectilSpeed, projectilRange, projectilDamage);
    }

    private void FinishAttack()
    {
        timeElapsed = 0;
        attackDone = false;
        turtle.attackPlayer = false;
    }
}
