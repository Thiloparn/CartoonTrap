using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public bool attacking = false;
    private bool grapping = false;
    private bool jumping = false;
    private bool jumpAbble = false;
    private bool healing = false;
    private bool usingBlade = false;
    private bool usingHammer = false;

    //Actions
    private IAction dash = new Dash();
    private IAction grap = new Grap();
    private IAction jump = new Jump();
    private IAction heal = new Heal();
    private IAction slash = new Slash();
    private IAction pum = new Pum();

    //Onos ¡¡TEMPORAL!!
    public GameObject attackOnomatopeya;

    private Rigidbody2D rigidBody;
    [SerializeField] PlayerInput playerInput;
    public string activeActionMap;

    public bool Attacking { get => attacking; set => attacking = value; }

    private void Awake()
    {
        playerHealth = new Health(maxHealth, currentHealth);
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        activeActionMap = playerInput.currentActionMap.name;

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

    public void onMovement(InputAction.CallbackContext value)
    {
        movingDirection = value.ReadValue<Vector2>().x;
    }

    public void onPunch(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Attacking = true;
        }
    }

    public void onGrap(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            grapping = true;
        }
    }

    public void onSlash(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            usingBlade = true;
        }
    }

    public void onPum(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            usingHammer = true;
        }
    }

    public void onHeal(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            healing = true;
        }
    }

    public void onJump(InputAction.CallbackContext value)
    {
        if (value.started && jumpAbble)
        {
            jumping = true;
        }
    }

    public void onDash(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            dashing = true;
        }
    }

    public void onChangeInputs(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if(playerInput.currentActionMap.name == "WASD without Mouse")
            {
                playerInput.SwitchCurrentActionMap("WASD with Mouse");
            }
            else if (playerInput.currentActionMap.name == "WASD with Mouse")
            {
                playerInput.SwitchCurrentActionMap("Arrows");
            }
            else
            {
                playerInput.SwitchCurrentActionMap("WASD without Mouse");
            }
        }

        Debug.Log("Cambiado mapeado de inputs");
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
