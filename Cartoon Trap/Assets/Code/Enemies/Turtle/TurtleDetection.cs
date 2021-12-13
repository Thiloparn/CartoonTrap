using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleDetection : MonoBehaviour
{
    public float detectionRadius = 5f;

    public bool playerDetected = false;
    public bool onoDetected = false;

    [SerializeField] Turtle turtle;
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
            turtle.player = GameObject.FindObjectOfType<PlayerController>();
        }

        if (collision.gameObject.tag == "Onomatopeya")
        {
            onoDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerDetected = false;
            turtle.player = null;
        }

        if (collision.gameObject.tag == "Onomatopeya")
        {
            onoDetected = false;
        }
    }
}
