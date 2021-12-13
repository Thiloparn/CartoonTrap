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
    private GameObject thrower;

    public void createProjectile(Vector2 _direction, float _speed, float _range, int _damage, GameObject _thrower)
    {
        direction = _direction;
        speed = _speed;
        range = _range;
        damage = _damage;
        thrower = _thrower;

        initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if(Vector2.Distance(initialPosition, transform.position) >= range || CheckObstacle())
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
        Destroy(this.gameObject);
    }

    private void Move()
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.fixedDeltaTime;
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
