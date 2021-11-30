using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    //Health
    public int maxHealth = 0;
    public int initialCurrentHealth = -1; //Variable temporal para poder probar la clase Health de manera comoda
    private Health enemyHealth;

    //Movement
    public float movingDirectionX = 0f;
    public float speed = 3f;

    //Detection
    public float detectionRadius = 5f;

    //Flags
    public bool playerDetected = false;

    private Rigidbody2D rigidBody;
    private PlayerController player;
    [SerializeField] CircleCollider2D detectionCollider;

    private void Awake()
    {
        enemyHealth = new Health(maxHealth, initialCurrentHealth);
        rigidBody = GetComponent<Rigidbody2D>();
        detectionCollider.radius = detectionRadius;

        int[] direction = { -1, 1 };
        movingDirectionX = direction[UnityEngine.Random.Range(0, direction.Length)];
        SpriteDirection(movingDirectionX);
    }

    private void FixedUpdate()
    {

        if (enemyHealth.CurrentHealth == 0)
        {
            Die();
        }
        else
        {
            if (playerDetected)
            {
                detectionCollider.radius = detectionRadius * 2;
                MoveToPlayer();
            }
            else
            {
                detectionCollider.radius = detectionRadius / 2;

                if (!CheckFloorAhead())
                {
                    movingDirectionX *= -1;
                }

                Move();
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
        transform.position += Vector3.right * movingDirectionX * speed * Time.fixedDeltaTime;
        SpriteDirection(movingDirectionX);
    }

    private void MoveToPlayer()
    {
        float directionOfPlayer = player.transform.position.x > transform.position.x ? 1f : -1f;

        transform.position += Vector3.right * directionOfPlayer * speed * Time.fixedDeltaTime;

        SpriteDirection(directionOfPlayer);
    }

    private bool CheckFloorAhead()
    {
        return Physics2D.Raycast(transform.position, transform.TransformDirection(new Vector2(movingDirectionX, -1)), 2, LayerMask.GetMask("Floor"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerDetected = true;
            player = GameObject.FindObjectOfType<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerDetected = false;
            player = null;
        }
    }

    private void Die()
    {
        throw new NotImplementedException();
    }
}
