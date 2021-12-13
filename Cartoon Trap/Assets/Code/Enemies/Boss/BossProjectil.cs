using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectil : MonoBehaviour
{
    public float direction = -1f;
    private float speed = 4f;
    private int damage = 1;

    private bool hittingPlayer = false;

    private PlayerController player;
    private GameObject thrower;

    public void createProjectile(float _direction, float _speed, int _damage, GameObject _thrower)
    {
        direction = _direction;
        speed = _speed;
        damage = _damage;
        thrower = _thrower;
    }

    private void FixedUpdate()
    {
        if (CheckObstacle())
        {
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

    private void Move()
    {
        transform.position += Vector3.right * direction * speed * Time.fixedDeltaTime;
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

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, new Vector2(direction, 0), 0.5f, LayerMask.GetMask("InGame"));

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.tag != "Player" && !hit.collider.gameObject.Equals(thrower)
                && !hit.collider.gameObject.Equals(this.gameObject) && !hit.collider.isTrigger)
            {
                res = true;
                break;
            }
        }


        return res;
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
