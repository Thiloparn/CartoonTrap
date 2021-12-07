using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public float detectionRadius = 5f;

    public bool playerDetected = false;

    [SerializeField] BasicEnemy basicEnemy;
    private CircleCollider2D detectionCollider;

    private void Awake()
    {
        detectionCollider = GetComponent<CircleCollider2D>();
        detectionCollider.radius = detectionRadius;
    }

    private void FixedUpdate()
    {
        if (playerDetected)
        {
            detectionCollider.radius = detectionRadius * 2;
        }
        else
        {
            detectionCollider.radius = detectionRadius;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerDetected = true;
            basicEnemy.player = GameObject.FindObjectOfType<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerDetected = false;
            basicEnemy.player = null;
        }
    }
}
