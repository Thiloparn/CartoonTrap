using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgehog : MonoBehaviour
{
    //Health
    public int maxHealth = 0;
    public int initialCurrentHealth = -1; //Variable temporal para poder probar la clase Health de manera comoda
    private Health enemyHealth;

    public float damageReduction = 2;
    public float stunDuration = 1f;
    private float stunTimer = 0f;

    //Movement
    private Vector2 initialPostion = new Vector2(0f, 0f);
    public float movingDirectionX = 1f;
    public float distance = 0f;
    public float speed = 3f;

    //Flags
    public bool moves = false;
    public bool followPlayer = false;
    public bool attackPlayer = false;
    private bool vulnerable = false;

    public PlayerController player;
    [SerializeField] HedgehogDetection hedgehogDetection;
    private BoxCollider2D boxCollider;
    private Animator anim;
    [SerializeField] AnimationClip deathAnimation;

    private void Awake()
    {
        enemyHealth = new Health(maxHealth, initialCurrentHealth);
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        initialPostion = transform.position;
    }

    private void FixedUpdate()
    {
        if (enemyHealth.CurrentHealth == 0)
        {
            Die();
        }
        else if (moves)
        {
            if (!vulnerable && hedgehogDetection.playerDetected)
            {
                TurnBackFromPlayer();
            }
            else if (vulnerable && hedgehogDetection.playerDetected)
            {
                FleeFromPlayer();
            }
            else if (followPlayer && hedgehogDetection.playerDetected)
            {
                MoveToPlayer();
            }
            else
            {
                Move();
            }
        }
        else
        {
            stunTimer++;

            if (stunTimer >= stunDuration)
            {
                stunTimer = 0f;
                moves = true;
            }
        }

        if (hedgehogDetection.playerDetected)
        {
            if (TouchingPlayer())
            {
                player.TakeDamage(1);
            }
        }
    }

    private void SpriteDirection(float direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = direction * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, initialPostion) >= distance || CheckObstacle())
        {
            initialPostion = transform.position;
            movingDirectionX *= -1;
        }

        transform.position += Vector3.right * movingDirectionX * speed * Time.fixedDeltaTime;
        SpriteDirection(movingDirectionX);
    }

    private void MoveToPlayer()
    {
        float directionOfPlayer = 1f;

        if (player.transform.position.x - 0.5f > transform.position.x)
        {
            directionOfPlayer = 1f;
            transform.position += Vector3.right * directionOfPlayer * speed * Time.fixedDeltaTime;
        }
        else if (player.transform.position.x + 0.5f < transform.position.x)
        {
            directionOfPlayer = -1f;
            transform.position += Vector3.right * directionOfPlayer * speed * Time.fixedDeltaTime;
        }

        SpriteDirection(directionOfPlayer);
    }

    private void FleeFromPlayer()
    {
        float fleeDirection = player.transform.position.x > transform.position.x ? -1f : 1f;

        transform.position += Vector3.right * fleeDirection * speed * Time.fixedDeltaTime;

        SpriteDirection(fleeDirection);
    }

    private void TurnBackFromPlayer()
    {
        float turnBackDirection = player.transform.position.x > transform.position.x ? -1f : 1f;

        SpriteDirection(turnBackDirection);
    }

    private bool CheckObstacle()
    {
        bool res = false;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.TransformDirection(new Vector2(movingDirectionX, 0)), 1, LayerMask.GetMask("InGame"));

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.tag != "Player" && !hit.collider.gameObject.Equals(this.gameObject) && !hit.collider.isTrigger)
            {
                res = true;
                break;
            }
        }


        return res;
    }

    private bool TouchingPlayer()
    {
        return boxCollider.IsTouching(player.CapsuleCollider);
    }

    public void TakeDamage(int damage, GameObject gameObject)
    {
        bool b = false;

        if (transform.localScale.x > 0)
        {
            b = gameObject.transform.position.x <= transform.position.x - (boxCollider.size.x * transform.localScale.x) / 2;
        }
        else
        {
            b = gameObject.transform.position.x >= transform.position.x + (boxCollider.size.x * transform.localScale.x) / 2;
        }

        if (b && !vulnerable)
        {
            enemyHealth.DecreaseHealth(Mathf.FloorToInt(damage / damageReduction));
            stunTimer = 0f;
            moves = false;

            if (gameObject.tag == "PumEffectArea")
            {
                Die();
            }
            else
            {
                attackPlayer = true;
            }
        }
        else
        {
            enemyHealth.DecreaseHealth(damage);
        }

    }

    private void Die()
    {
        anim.SetBool("Defend", false);
        anim.SetTrigger("Dead");
        Destroy(this.gameObject, deathAnimation.length);
    }
}
