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
    private Vector2 initialPostion = new Vector2(0f, 0f);
    public float movingDirectionX = 1f;
    public float distance = 0f;
    public float speed = 3f;

    //Flags
    public bool moves = false;
    public bool followPlayer = false;
    public bool attackPlayer = false;

    private Rigidbody2D rigidBody;
    public PlayerController player;
    [SerializeField] EnemyDetection enemyDetection;

    private void Awake()
    {
        enemyHealth = new Health(maxHealth, initialCurrentHealth);
        rigidBody = GetComponent<Rigidbody2D>();
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
            if (enemyDetection.playerDetected && followPlayer)
            {
                MoveToPlayer();
            }
            else
            {
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

        if(player.transform.position.x - 0.5f > transform.position.x)
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

    private bool CheckObstacle()
    {
        bool res = false;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.TransformDirection(new Vector2(movingDirectionX, 0)), 1, LayerMask.GetMask("InGame"));

        foreach(RaycastHit2D hit in hits)
        {
            if(hit.collider.gameObject.tag != "Player" && !hit.collider.gameObject.Equals(this.gameObject) && !hit.collider.isTrigger)
            {
                res = true;
                break;
            }
        }


        return res;
    }

    private void Die()
    {
        throw new NotImplementedException();
    }
}
