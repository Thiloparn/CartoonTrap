using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    //Health
    public int maxHealth = 0;
    public int initialCurrentHealth = -1; //Variable temporal para poder probar la clase Health de manera comoda
    private Health enemyHealth;

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
    public bool hidden = false;

    public PlayerController player;
    [SerializeField] TurtleDetection turtleDetection;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        enemyHealth = new Health(maxHealth, initialCurrentHealth);
        boxCollider = GetComponent<BoxCollider2D>();
        initialPostion = transform.position;
    }

    private void FixedUpdate()
    {
        if (hidden)
        {
            if (!turtleDetection.playerDetected && !turtleDetection.onoDetected)
            {
                UnHide();
            }
        }
        else
        {
            if (enemyHealth.CurrentHealth == 0)
            {
                Die();
            }
            else if (moves)
            {
                if (turtleDetection.playerDetected)
                {
                    if (!vulnerable)
                    {
                        Hide();
                    }
                    
                    if (TouchingPlayer())
                    {
                        player.TakeDamage(1);
                    }
                }

                if (turtleDetection.onoDetected && !vulnerable)
                {
                    Hide();
                }

                if (vulnerable && turtleDetection.playerDetected)
                {
                    FleeFromPlayer();
                }
                else if (followPlayer && turtleDetection.playerDetected)
                {
                    MoveToPlayer();
                }
                else
                {
                    Move();
                }
            }
        }
    }

    private void SpriteDirection(float direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = direction;
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

    private void Hide()
    {
        hidden = true;
    }

    private void UnHide()
    {
        hidden = false;
    }

    public void TakeDamage(int damage, GameObject gameObject)
    {
        if (hidden)
        {
            if (gameObject.tag == "PumEffectArea")
            {
                hidden = false;
                vulnerable = true;
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
        Destroy(this.gameObject);
    }
}
