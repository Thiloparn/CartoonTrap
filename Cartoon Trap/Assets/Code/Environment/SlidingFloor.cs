using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingFloor : MonoBehaviour
{
    public float slidingSpeed = 1f;
    public float direction = 1f;


    private PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            direction = player.MovingDirectionX != 0 ? player.MovingDirectionX: direction;
            player.Sliding = true;
            player.JumpAbble = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.Sliding = false;
            player.JumpAbble = true;
            player = null;

        }
    }

    private void OnTriggerStay2D(Collider2D collision)//FixedUpdate()
    {
        if (player != null && collision.tag == "Player")
        {
            
                if (!CheckObstacle() /*&& player.MovingDirectionX != 0*/)
                {
                    player.Sliding = true;
                    player.transform.position += Vector3.right * direction * slidingSpeed * Time.fixedDeltaTime;
                }
                else
                {
                    player.Sliding = false;
                    player.JumpAbble = true;
                }
            

        }
    }

    private bool CheckObstacle()
    {
        bool res = false;

        RaycastHit2D[] hits = Physics2D.RaycastAll(player.transform.position, transform.TransformDirection(new Vector2(direction, 0)), 0.5f, LayerMask.GetMask("InGame"));

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.tag != "Player" /* !hit.collider.gameObject.Equals(this.gameObject)*/ && !hit.collider.isTrigger)
            {
                res = true;
                break;
            }
        }


        return res;
    }
}
