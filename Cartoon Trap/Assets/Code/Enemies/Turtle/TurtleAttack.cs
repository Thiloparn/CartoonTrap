using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAttack : MonoBehaviour
{
    public float resetTimer = 0.5f;
    public float timeElapsed = 0f;
    public float attackOn = 0.25f;
    public int attackOnFrame = 1;

    public float projectilRange = 5f;
    public int projectilDamage = 1;
    public float projectilSpeed = 4f;

    private bool attackDone = false;

    private PlayerController player;
    private CircleCollider2D attackCollider;
    [SerializeField] Turtle turtle;
    [SerializeField] Projectil projectil;
    [SerializeField] Animator anim;

    private void Awake()
    {
        attackCollider = GetComponent<CircleCollider2D>();
        attackOn = attackOnFrame / 60f;
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
        anim.SetTrigger("Attack");
        timeElapsed += Time.fixedDeltaTime;
    }

    private void DoAttack()
    {
        Projectil proj1; 
        Projectil proj2;
        Projectil proj3;
        Projectil proj4;
        Projectil proj5;

        if (turtle.transform.localScale.x == 1f)
        {
            

            proj1 = Instantiate(projectil, transform.position, Quaternion.identity);
            proj2 = Instantiate(projectil, transform.position, Quaternion.Euler(0, 0, 45));
            proj3 = Instantiate(projectil, transform.position, Quaternion.Euler(0, 0, 90));
            proj4 = Instantiate(projectil, transform.position, Quaternion.Euler(0, 0, 135));
            proj5 = Instantiate(projectil, transform.position, Quaternion.Euler(0, 0, 180));
        }
        else
        {
            proj1 = Instantiate(projectil, transform.position, Quaternion.Euler(0, 0, 180));
            proj2 = Instantiate(projectil, transform.position, Quaternion.Euler(0, 0, 135));
            proj3 = Instantiate(projectil, transform.position, Quaternion.Euler(0, 0, 90));
            proj4 = Instantiate(projectil, transform.position, Quaternion.Euler(0, 0, 45));
            proj5 = Instantiate(projectil, transform.position, Quaternion.identity);
        }

        proj1.createProjectile(new Vector2(turtle.transform.localScale.x, 0), projectilSpeed, projectilRange, projectilDamage, turtle.gameObject);

        proj2.createProjectile(new Vector2(turtle.transform.localScale.x, Mathf.Abs(turtle.transform.localScale.x)), projectilSpeed, projectilRange, projectilDamage, turtle.gameObject);

        proj3.createProjectile(new Vector2(0, Mathf.Abs(turtle.transform.localScale.x)), projectilSpeed, projectilRange, projectilDamage, turtle.gameObject);

        proj4.createProjectile(new Vector2(-turtle.transform.localScale.x, Mathf.Abs(turtle.transform.localScale.x)), projectilSpeed, projectilRange, projectilDamage, turtle.gameObject);

        proj5.createProjectile(new Vector2(-turtle.transform.localScale.x, 0), projectilSpeed, projectilRange, projectilDamage, turtle.gameObject);
    }

    private void FinishAttack()
    {
        timeElapsed = 0;
        attackDone = false;
        turtle.attackPlayer = false;
    }
}
