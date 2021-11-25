using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    //Health
    public int maxHealth = 0;
    public int currentHealth = -1; //Variable temporal para poder probar la clase Health de manera comoda
    public Health playerHealth;

    //Movement
    private float movingDirection = 0f;
    public float speed = 5f;
    public float jumpForce = 5f;

    //Flags
    private bool dashing = false;
    private bool attacking = false;
    private bool grapping = false;
    private bool jumping = false;
    private bool jumpAbble = false;
    private bool healing = false;
    private bool usingBlade = false;
    private bool usingHammer = false;

    //Actions
    private IAction dash = new Dash();
    private IAction attack = new Attack();
    private IAction grap = new Grap();
    private IAction jump = new Jump();
    private IAction heal = new Heal();
    private IAction slash = new Slash();
    private IAction pum = new Pum();

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        playerHealth = new Health(maxHealth, currentHealth);
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ManageInputs();
    }

    private void FixedUpdate()
    {
        if (playerHealth.currentHealth == 0)
        {
            Die();
        }
        else
        {
            if (isGrounded())
            {
                jumpAbble = true;
            }
            else
            {
                jumpAbble = false;
            }

            ExecuteActions();
        }
    }

    private void ExecuteActions()
    {
        if (dashing)
        {
            dash.ExecuteAction(this);
            dashing = false;
        }

        if (attacking)
        {
            attack.ExecuteAction(this);
            attacking = false;
        }

        if (grapping)
        {
            grap.ExecuteAction(this);
            grapping = false;
        }

        if (jumping)
        {
            jump.ExecuteAction(this);
            jumping = false;
        }

        if (healing)
        {
            heal.ExecuteAction(this);
            healing = false;
        }

        if (usingBlade)
        {
            slash.ExecuteAction(this);
            usingBlade = false;
        }

        if (usingHammer)
        {
            pum.ExecuteAction(this);
            usingHammer = false;
        }

        Move();
    }

    private void Die()
    {
        throw new NotImplementedException();
    }

    private void ManageInputs()
    {
        movingDirection = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Dash"))
        {
            dashing = true;
        }

        if (Input.GetButtonDown("BasicAttack"))
        {
            attacking = true;
        }

        if (Input.GetButtonDown("Grap"))
        {
            grapping = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (jumpAbble)
            {
                jumping = true;
            }
        }

        if (Input.GetButtonDown("Heal"))
        {
            healing = true;
        }

        if (Input.GetButtonDown("Slash"))
        {
            usingBlade = true;
        }

        if (Input.GetButtonDown("Pum"))
        {
            usingHammer = true;
        }
    }

    private void Move()
    {
        transform.position += Vector3.right * movingDirection * speed * Time.fixedDeltaTime;
    }

    public bool isGrounded()
    {
        if (Math.Abs((rigidBody.velocity.y)) < 0.001f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
