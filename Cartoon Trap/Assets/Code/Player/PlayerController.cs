using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 0;
    public int currentHealth = -1; //Variable temporal para poder probar la clase Health de manera comoda
    public Health playerHealth;

    private float movingDirection = 0f;
    private float speed = 5f;
    private bool dashing = false;
    private bool attacking = false;
    private bool grapping = false;
    private bool jumping = false;
    private bool healing = false;
    private bool usingBlade = false;
    private bool usingHammer = false;

    private IAction dash = new Dash();
    private IAction attack = new Attack();
    private IAction grap = new Grap();
    private IAction jump = new Jump();
    private IAction heal = new Heal();
    private IAction slash = new Slash();
    private IAction pum = new Pum();

    private void Awake()
    {
        playerHealth = new Health(maxHealth, currentHealth);
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
            jumping = true;
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
}
