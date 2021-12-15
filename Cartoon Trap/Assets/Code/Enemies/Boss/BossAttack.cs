using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float resetTimer = 0.5f;
    public float resetTimerIncrementPerState = 0.5f;
    public float timeElapsed = 0f;
    public int attackOnFrame = 1;
    private float attackOn = 0;
    private int numAttacksDone = 0;
    public int projectilDamage = 1;
    public float projectilSpeed = 4f;
    public float projectilSpeedIncrementPerState = 2f;

    public List<float> attacksHeight = new List<float>();

    private bool attackDone = false;

    [SerializeField] Boss boss;
    [SerializeField] List<BossProjectil> projectils = new List<BossProjectil>();
    [SerializeField] Animator anim;

    private void Awake()
    {
        attackOn = attackOnFrame / 60f;
    }

    private void FixedUpdate()
    {
        if (timeElapsed == 0 && boss.attackPlayer && !boss.moves && numAttacksDone < boss.attacksPerState)
        {
            StartAttack();
        }
        else if (timeElapsed > 0)
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
        else if (numAttacksDone >= boss.attacksPerState)
        {
            boss.Tired();
            numAttacksDone = 0;
        }
    }

    private void StartAttack()
    {
        anim.SetBool("Attack", true);
        numAttacksDone++;
        timeElapsed += Time.fixedDeltaTime;
    }

    private void DoAttack()
    {
        BossProjectil proj;

        int rand1 = Random.Range(0, attacksHeight.Count);
        Vector3 initialPosition = new Vector3(boss.transform.position.x, boss.transform.position.y + attacksHeight[rand1], 0);

        int rand2 = Random.Range(0, projectils.Count);
        BossProjectil projectil = projectils[rand2];

        if (boss.transform.localScale.x > 0)
        {
            proj = Instantiate(projectil, initialPosition, Quaternion.identity);
        }
        else
        {
            proj = Instantiate(projectil, initialPosition, Quaternion.Inverse(Quaternion.identity));
        }

        proj.createProjectile(boss.transform.localScale.x/Mathf.Abs(boss.transform.localScale.x), projectilSpeed, projectilDamage, boss.gameObject);
        anim.SetBool("Attack", false);
    }

    private void FinishAttack()
    {
        timeElapsed = 0;
        attackDone = false;
    }
}
