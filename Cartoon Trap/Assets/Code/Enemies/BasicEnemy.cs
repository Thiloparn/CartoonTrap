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

    //Flags
    public bool attacking = false;

    private Rigidbody2D rigidBody;

    public bool Attacking { get => attacking; set => attacking = value; }

    private void Awake()
    {
        enemyHealth = new Health(maxHealth, initialCurrentHealth);
        rigidBody = GetComponent<Rigidbody2D>();

        int[] direction = { -1, 1 };
        movingDirectionX = direction[UnityEngine.Random.Range(0, direction.Length)];
        if(movingDirectionX == -1)
        {
            TurnSprite();
        }
    }

    private void Update()
    {
        //print(enemyHealth.CurrentHealth);
    }

    private void FixedUpdate()
    {

        if (enemyHealth.CurrentHealth == 0)
        {
            Die();
        }
        else
        {

            if (!CheckFloorAhead())
            {
                movingDirectionX *= -1;
                TurnSprite();
            }

            Move();
        }
    }

    private void TurnSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Die()
    {
        throw new NotImplementedException();
    }

    private void Move()
    {
        transform.position += Vector3.right * movingDirectionX * speed * Time.fixedDeltaTime;
    }

    private bool CheckFloorAhead()
    {
        return Physics2D.Raycast(transform.position, transform.TransformDirection(new Vector2(movingDirectionX, -1)), 2, LayerMask.GetMask("Floor"));
    }
}
