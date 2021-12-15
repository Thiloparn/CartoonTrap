using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //Movement
    public Vector2 rightPostion = new Vector2(0f, 0f);
    public Vector2 leftPostion = new Vector2(0f, 0f);
    public float movingDirectionX = -1f;
    public float speed = 3f;
    public float speedIncrementPerState = 1f;

    public float brokenDuration = 1f;
    public float tiredDuration = 10f;
    private float stunDuration = 0f;
    private float stunTimer = 0f;

    //Flags
    public bool moves = false;
    public bool playerDetected = false;
    public bool attackPlayer = false;
    public bool stunned = false;
    public bool killed = false;

    //States
    private List<string> states = new List<string>();
    private string currentState = "";

    //Attacks
    public int attacksPerState = 3;
    public int attackIncrementPerState = 2;

    public PlayerController player;
    private BoxCollider2D boxCollider;
    [SerializeField] BossAttack bossAttack;
    public Animator anim;
    private new Rigidbody2D rigidbody;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

        SpriteDirection(movingDirectionX);

        states.Add("Blue");
        states.Add("Green");
        states.Add("Black");

        currentState = "Blue";
    }

    private void FixedUpdate()
    {
        if (killed)
        {
            Die();
        }
        else if (stunned)
        {
            stunTimer += Time.fixedDeltaTime;

            if (stunTimer >= stunDuration)
            {
                EndStun();
            }
        }
        else if (moves)
        {
            Move();
        }

        if (playerDetected)
        {
            if (TouchingPlayer())
            {
                player.TakeDamage(1);
                player.playerAnimator.StartHurtingAnimation(player);
            }
        }
    }

    private void SpriteDirection(float direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = direction * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    private void ChangePosition()
    {
        if (Vector2.Distance(transform.position, leftPostion) <= 1f)
        {
            movingDirectionX = 1f;
        }
        else if (Vector2.Distance(transform.position, rightPostion) <= 1f)
        {
            movingDirectionX = -1f;
        }
        moves = true;
    }

    private void Move()
    {
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        anim.SetBool("Walking", true);
        transform.position += Vector3.right * movingDirectionX * speed * Time.fixedDeltaTime;
        
        if (movingDirectionX == 1f)
        {
            if(Vector2.Distance(transform.position, rightPostion) <= 1f)
            {
                moves = false;
                anim.SetBool("Walking", false);
                rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                SpriteDirection(-movingDirectionX);
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, leftPostion) <= 1f)
            {
                moves = false;
                anim.SetBool("Walking", false);
                rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
                SpriteDirection(-movingDirectionX);
            }
        }
    }

    public void BeginStun(float duration)
    {
        anim.SetBool("Tired", true);
        stunned = true;
        attackPlayer = false;

        stunDuration = duration;
        stunTimer = 0;
    }

    public void EndStun()
    {
        anim.SetBool("Tired", false);
        stunned = false;
        attackPlayer = true;

        stunTimer = 0;
    }

    public void Tired()
    {
        BeginStun(tiredDuration);
    }

    private bool TouchingPlayer()
    {
        return boxCollider.IsTouching(player.CapsuleCollider);
    }

    public void TakeDamage(int damage, GameObject gameObject)
    {
        if (stunned)
        {
            bool pum = gameObject.tag == "PumEffectArea" && currentState == "Green";
            bool slash = gameObject.tag == "SlashEffectArea" && currentState == "Blue";
            bool punch = gameObject.tag == "PunchEffectArea" && currentState == "Black";

            if (pum || slash || punch)
            {
                NextState();
                ChangePosition();
                BeginStun(brokenDuration);
            }
        }
    }

    private void NextState()
    {
        if (currentState == "Black")
        {
            killed = true;
        }
        else
        {
            for (int i = 0; i < states.Count; i++)
            {
                if (states[i] == currentState)
                {
                    currentState = states[i + 1];
                    attacksPerState += attackIncrementPerState;
                    speed += speedIncrementPerState;
                    bossAttack.resetTimer += bossAttack.resetTimerIncrementPerState;
                    bossAttack.projectilSpeed += bossAttack.projectilSpeedIncrementPerState;
                    break;
                }
            }
        }
    }

    private void Die()
    {
        //anim.SetTrigger("Dead");
        //Destroy(this.gameObject, anim.GetCurrentAnimatorStateInfo(0).length);

        Destroy(this.gameObject);
    }
}
