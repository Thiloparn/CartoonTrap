using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour
{
    private Vector2 direction = new Vector2(1, 0);
    private float speed = 4f;
    private float range = 5f;
    private int damage = 1;
    private Vector2 initialPosition;

    private bool hittingPlayer = false;

    private PlayerController player;

    public void createProjectile(Vector2 _direction, float _speed, float _range, int _damage)
    {
        direction = _direction;
        speed = _speed;
        range = _range;
        damage = _damage;

        initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if(Vector2.Distance(initialPosition, transform.position) >= range)
        {
            Destroy(this.gameObject);
        }
        else if(hittingPlayer)
        {
            DamagePlayer();
        }
        else
        {
            Move();
        }
    }

    private void DamagePlayer()
    {
        player.TakeDamage(damage);
        player.playerAnimator.StartHurtingAnimation(player);
        Destroy(this.gameObject);
    }

    private void Move()
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.fixedDeltaTime;
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
