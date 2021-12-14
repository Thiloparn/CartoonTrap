using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDetection : MonoBehaviour
{
    [SerializeField] Boss boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            boss.playerDetected = true;
            boss.attackPlayer = true;
            boss.player = GameObject.FindObjectOfType<PlayerController>();
        }
    }
}
