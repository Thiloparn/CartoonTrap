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
    public int initialCurrentHealth = -1; //Variable temporal para poder probar la clase Health de manera comoda
    private Health playerHealth;
    public int numberOfHealings = 3;

    //Movement
    private float movingDirectionX = 0f;
    private float movingDirectionY = 0f;
    public float speed = 5f;
    public float jumpForce = 5f;

    //Combat
    public float attackPower = 0;

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
    private Heal heal;
    private IAction slash = new Slash();
    private IAction pum = new Pum();

    //Onos ¡¡TEMPORAL!!
    public GameObject attackOnomatopeya;
    public GameObject phiuOnomatopeya;
    public GameObject pumOnomatopeya;

    private Rigidbody2D rigidBody;
    [SerializeField] PlayerInput playerInput;
    public string activeActionMap;

    public bool Attacking { get => attacking; set => attacking = value; }
    public float MovingDirectionX { get => movingDirectionX;}
    public float MovingDirectionY { get => movingDirectionY;}

    private void Awake()
    {
        playerHealth = new Health(maxHealth, initialCurrentHealth);
        rigidBody = GetComponent<Rigidbody2D>();
        heal = new Heal(numberOfHealings);
    }

    private void FixedUpdate()
    {
        activeActionMap = playerInput.currentActionMap.name;

        if (playerHealth.CurrentHealth == 0)
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
        movingDirectionX = value.ReadValue<Vector2>().x;
        movingDirectionY = value.ReadValue<Vector2>().y;
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
        transform.position += Vector3.right * movingDirectionX * speed * Time.fixedDeltaTime;
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

    public void Heal()
    {
        playerHealth.IncreaseHealth(1);
    }

    public void Rest()
    {
        playerHealth.ResetHealth();
        heal.resetHealings();
    }
}
