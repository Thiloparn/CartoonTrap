using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{

    //Health
    public int maxHealth = 0;
    public int initialCurrentHealth = -1; //Variable temporal para poder probar la clase Health de manera comoda
    private Health playerHealth;
    public int numberOfHealings = 3;
    public bool invulneravility = false;
    public float maxTimeInvulneravility = 1f;
    private float currentTimeInvulneravility = 0f;
    private bool reviveAbble = false;

    //Movement
    private float movingDirectionX = 0f;
    private float movingDirectionY = 0f;
    public float speed = 5f;
    public float jumpForce = 5f;
    public float reboundForce = 2f;
    public float doubleJumpForce = 2f;
    private float lookingDirection = 1f;

    //Dash
    private float timeDashing = 0;
    public float dashDuration;
    public float dashSpeed;

    //Rest
    private float restingTime = 2f;
    private float standingUpTime = 0.5f;
    private float restingTimeElapsed = 0;

    //Combat
    public int attackPower = 0;
    public int hammerPower = 0;
    public int bladePower = 0;

    //Flags
    private bool dashing = false;
    private bool dashAbble = false;
    private bool attacking = false;
    public bool grapping = false;
    public bool throwing = false;
    private bool jumping = false;
    private bool jumpAbble = false;
    private bool doubleJumping = false;
    private bool doubleJumpAbble = false;
    private bool healing = false;
    private bool usingBlade = false;
    private bool usingHammer = false;
    public bool resting = false;
    private bool dead = false;

    //Actions
    private Dash dash = new Dash(0);
    private IAction jump = new Jump();
    public IAction rebound = new Rebound();
    private Heal heal;
    private IAction doubleJump = new DoubleJump();

    //Onos ¡¡TEMPORAL!!
    public GameObject attackOnomatopeya;
    public GameObject phiuOnomatopeya;
    public GameObject pumOnomatopeya;
    public GameObject slashOnomatpeya;
    public GameObject hopOnomatopeya;

    public PlayerAnimator playerAnimator;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    [SerializeField] PlayerInput playerInput;
    public string activeActionMap;
    public PlayerPocket pocket;
    public GameObject deathCanvas;

    public bool Attacking { get => attacking; set => attacking = value; }
    public bool Grapping { get => grapping; set => grapping = value; }
    public float MovingDirectionX { get => movingDirectionX;}
    public float MovingDirectionY { get => movingDirectionY;}
    public Rigidbody2D RigidBody { get => rigidBody;}

    public BoxCollider2D BoxCollider { get => boxCollider; }
    public bool Throwing { get => throwing; set => throwing = value; }
    public bool UsingBlade { get => usingBlade; set => usingBlade = value; }
    public bool UsingHammer { get => usingHammer; set => usingHammer = value; }

    private void Awake()
    {
        playerHealth = new Health(maxHealth, initialCurrentHealth);
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        heal = new Heal(numberOfHealings);
        playerAnimator = new PlayerAnimator();
        pocket = new PlayerPocket(this);
        lookingDirection = 1f;
        invulneravility = false;
    }

    private void Update()
    {
        playerAnimator.UpdateLookingDirection(this);
        playerAnimator.UpdateVelocityY(this);

        if (Math.Abs(movingDirectionX) >= 0.01f) //Fuente de bugs
        {
            playerAnimator.StartWalkingAnimation(this);
        }
        else
        {
            playerAnimator.EndWalkingAnimation(this);
        }
    }

    private void FixedUpdate()
    {
        activeActionMap = playerInput.currentActionMap.name;

        UpdateInvulneravility();

        if (playerHealth.CurrentHealth == 0)
        {
            if (!dead)
            {
                StartCoroutine(Die());
            }
        }
        else
        {
            UpdateLookingDirection();

            if (isGrounded())
            {
                jumpAbble = true;
                dashAbble = true;
                doubleJumpAbble = true;
            }
            else
            {
                jumpAbble = false;
            }

            if (!resting)
            {
                ExecuteActions();
            }
            else
            {
                restingTimeElapsed += Time.fixedDeltaTime;

                if (restingTimeElapsed >= restingTime)
                {
                    restingTimeElapsed = 0;
                    resting = false;
                }
                else if (restingTimeElapsed >= restingTime - standingUpTime)
                {
                    playerAnimator.EndRestingAnimation(this);
                }
            }
        }

        
    }

    public void UpdateLookingDirection()
    {
        if (movingDirectionX < -0.01f)
        {
            lookingDirection = -1f;
        }
        else if (movingDirectionX > 0.01f)
        {
            lookingDirection = 1f;
        }
    }

    private void UpdateInvulneravility()
    {
        if (invulneravility && !dead)
        {
            currentTimeInvulneravility += Time.fixedDeltaTime;

            if (currentTimeInvulneravility >= maxTimeInvulneravility)
            {
                currentTimeInvulneravility = 0;
                invulneravility = false;
            }
        }
    }

    private void ExecuteActions()
    {

        if (dashing)
        {
            if (timeDashing == 0)
            {
                playerAnimator.StartDashingAnimation(this);
            }

            timeDashing += Time.fixedDeltaTime;

            if (timeDashing >= dashDuration)
            {
                dashing = false;
                timeDashing = 0;
                dash.EndExecuteAction(this);
            }
            else
            {
                dash.ExecuteAction(this);
            }

        }
        else
        {
            if (healing)
            {
                heal.ExecuteAction(this);
                healing = false;
            }

            if (jumping)
            {
                jump.ExecuteAction(this);
                jumping = false;
            }else if (doubleJumping)
            {
                doubleJump.ExecuteAction(this);
                doubleJumpAbble = false;
                doubleJumping = false;
            }

            Move();
        }

    }

    private IEnumerator Die()
    {
        invulneravility = true;
        dead = true;

        yield return new WaitForSeconds(2.5f);
        reviveAbble = true;
        deathCanvas.SetActive(true);
    }

    private IEnumerator Revive()
    {
        Rest();
        reviveAbble = false;
        dead = false;
        SetRevivePosition();

        yield return new WaitForSeconds(0.5f);
        deathCanvas.SetActive(false);
    }

    private void SetRevivePosition()
    {
        if (GameData.lastRestZone != null)
        {
            gameObject.transform.position = GameData.lastRestZone.position;
        }
        else
        {
            SceneManager.LoadScene("Main Menu");
        }
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
            if (pocket.IsEmpty())
            {
                grapping = true;
            }
            else
            {
                throwing = true;
            }
            
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
            //Revive
            if (reviveAbble)
            {
                StartCoroutine(Revive());
            }
            else
            {
                healing = true;
            }
            
        }
        
    }

    public void onJump(InputAction.CallbackContext value)
    {
        if (value.started && jumpAbble)
        {
            jumping = true;
            doubleJumpAbble = true;
        }
        else if(value.started && !isGrounded() && doubleJumpAbble)
        {
            doubleJumping = true;
        }
    }

    public void onDash(InputAction.CallbackContext value)
    {
        if (value.started && dashAbble)
        {
            dashing = true;
            dashAbble = false;
            dash = new Dash(LookingAtDirection());
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

    public void TakeDamage(int damageAmount)
    {
        if(invulneravility == false)
        {
            playerHealth.DecreaseHealth(damageAmount);
            playerAnimator.StartHurtingAnimation(this);
            if (playerHealth.CurrentHealth == 0)
            {
                playerAnimator.StartDyingAnimation(this);
            }
            invulneravility = true;
        }
    }

    public void Rest()
    {
        playerHealth.ResetHealth();
        heal.resetHealings();
        resting = true;
        playerAnimator.StartRestingAnimation(this);
    }

    public float LookingAtDirection()
    {
        return lookingDirection;
    }
}
