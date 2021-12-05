using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            print("Rest");
            player.Rest();
        }
    }
}