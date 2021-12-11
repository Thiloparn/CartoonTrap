using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushThorns : MonoBehaviour
{
    public float duration = 5f;
    private float timer = 0f;
    public int damage = 1;

    public void Update()
    {
        if (timer >= duration)
        {
            Destroy(this.gameObject);
        }

        timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            player.TakeDamage(damage);
            player.playerAnimator.StartHurtingAnimation(player);
        }
    }
}
