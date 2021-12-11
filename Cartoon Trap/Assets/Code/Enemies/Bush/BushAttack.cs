using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushAttack : MonoBehaviour
{
    public float resetTimer = 0.5f;
    public float timeElapsed = 0f;
    public float attackOn = 0.25f;
    public float range = 0.4f;
    public float projectilRange = 5f;
    public int projectilDamage = 1;
    public float projectilSpeed = 4f;

    private bool onRange = false;
    private bool attackDone = false;

    private PlayerController player;
    private BoxCollider2D attackCollider;
    [SerializeField] Bush bush;
    [SerializeField] BushProjectil projectil;

    private void Awake()
    {
        attackCollider = GetComponent<BoxCollider2D>();
        attackCollider.size = new Vector2(range, attackCollider.size.y);
        attackCollider.offset = new Vector2(range / 2, 0);
    }

    private void FixedUpdate()
    {
        if (timeElapsed == 0 && bush.attackPlayer)
        {
            if (onRange)
            {
                StartAttack();
            }
        }
        else if (timeElapsed > 0)
        {
            if (timeElapsed < resetTimer)
            {
                if (timeElapsed >= attackOn && !attackDone && onRange)
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
        BushProjectil proj;

        if (bush.transform.localScale.x == 1f)
        {
            proj = Instantiate(projectil, bush.transform.position, Quaternion.identity);
        }
        else
        {
            proj = Instantiate(projectil, bush.transform.position, Quaternion.Inverse(Quaternion.identity));
        }

        proj.createProjectile(player.transform.position, projectilSpeed, projectilRange, projectilDamage, bush.gameObject);
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
