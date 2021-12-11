using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushProjectil : MonoBehaviour
{
    private Vector2 direction = new Vector2(1, 0);
    private float speed = 4f;
    private float range = 5f;
    private int damage = 1;
    private Vector2 initialPosition;

    private float speedY = 0f;
    private float flightTimer = 0f;

    private bool hittingPlayer = false;
    private bool hitFloor = false;

    private PlayerController player;
    private GameObject thrower;
    [SerializeField] BushThorns thorns;

    public void createProjectile(Vector2 _direction, float _speed, float _range, int _damage, GameObject _thrower)
    {
        direction = _direction;
        speed = _speed;
        range = _range;
        damage = _damage;
        thrower = _thrower;

        initialPosition = transform.position;

        CalculateTrajectory();
    }

    private void FixedUpdate()
    {
        if (CheckObstacle())
        {
            if (hitFloor)
            {
                CreateThorns();
            }

            Destroy(this.gameObject);
        }
        else if (hittingPlayer)
        {
            DamagePlayer();
        }
        else
        {
            Move();
        }
    }

    private void CalculateTrajectory()
    {
        float distanceX;

        if(initialPosition.x > direction.x)
        {
            distanceX = Mathf.Abs(initialPosition.x - direction.x);
        }
        else
        {
            distanceX = Mathf.Abs(direction.x - initialPosition.x);
        }

        float duration = distanceX / speed;

        float distanceY;

        if (initialPosition.y > direction.y)
        {
            distanceY = Mathf.Abs(initialPosition.y - direction.y);
        }
        else
        {
            distanceY = Mathf.Abs(direction.y - initialPosition.y);
        }

        speedY = (distanceY / duration) - ((Physics2D.gravity.y * duration) / 2);
    }

    private void Move()
    {
        float verticalSpeed = speedY + Physics2D.gravity.y * flightTimer;

        float horizontalSpeed = initialPosition.x > direction.x ? -speed: speed;

        transform.position += new Vector3(horizontalSpeed, verticalSpeed, 0) * Time.fixedDeltaTime;

        flightTimer += Time.fixedDeltaTime;
    }

    private void DamagePlayer()
    {
        player.TakeDamage(damage);
        player.playerAnimator.StartHurtingAnimation(player);
        Destroy(this.gameObject);
    }

    private bool CheckObstacle()
    {
        bool res = false;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.TransformDirection(direction), 0.5f, LayerMask.GetMask("InGame"));

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.tag != "Player" && !hit.collider.gameObject.Equals(thrower)
                && !hit.collider.gameObject.Equals(this.gameObject) && !hit.collider.isTrigger)
            {
                if (hit.collider.tag == "Floor")
                {
                    hitFloor = true;
                }

                res = true;
                break;
            }
        }


        return res;
    }

    private void CreateThorns()
    {
        BushThorns thr = Instantiate(thorns, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hittingPlayer = true;
            player = GameObject.FindObjectOfType<PlayerController>();
        }
    }
}
