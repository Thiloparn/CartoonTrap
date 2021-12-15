using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogDetection : MonoBehaviour
{
    public float detectionRadius = 5f;

    public bool playerDetected = false;

    [SerializeField] Hedgehog hedgehog;
    private CircleCollider2D detectionCollider;
    [SerializeField] Animator anim;

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
            anim.SetBool("Defend", true);
            hedgehog.player = GameObject.FindObjectOfType<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerDetected = false;
            anim.SetBool("Defend", false);
            hedgehog.player = null;
        }
    }
}
