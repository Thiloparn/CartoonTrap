using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
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
    [SerializeField] MoleDetection moleDetection;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        enemyHealth = new Health(maxHealth, initialCurrentHealth);
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        initialPostion = transform.position;
    }

    private void FixedUpdate()
    {
        if (hidden)
        {
            if (!moleDetection.playerDetected && !moleDetection.onoDetected)
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
                if (moleDetection.playerDetected)
                {
                    Hide();

                    if (TouchingPlayer())
                    {
                        player.TakeDamage(1);
                    }
                }

                if (moleDetection.onoDetected)
                {
                    Hide();
                }

                if (vulnerable && moleDetection.playerDetected)
                {
                    FleeFromPlayer();
                }
                else if (followPlayer && moleDetection.playerDetected)
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
        boxCollider.enabled = false;
        rigidBody.constraints = RigidbodyConstraints2D.FreezePositionY;

        //Temporal hasta que se tenga la animación
        transform.position = new Vector3(transform.position.x, transform.position.y - boxCollider.size.y/2, 0);
    }

    private void UnHide()
    {
        hidden = false;
        boxCollider.enabled = true;
        rigidBody.constraints = RigidbodyConstraints2D.None;

        //Temporal hasta que se tenga la animación
        transform.position = new Vector3(transform.position.x, transform.position.y + boxCollider.size.y/2, 0);
    }

    public void TakeDamage(int damage, GameObject gameObject)
    {
        enemyHealth.DecreaseHealth(damage);
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
