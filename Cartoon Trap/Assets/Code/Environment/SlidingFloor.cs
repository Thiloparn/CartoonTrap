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
            direction = player.MovingDirectionX;
            player.Sliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            player.Sliding = false;
        }
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            if (player.Sliding)
            {
                player.transform.position += Vector3.right * direction * slidingSpeed * Time.fixedDeltaTime;
            }

        }
    }
}
