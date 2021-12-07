using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Onomatopeya))]
public class ThrowedOno : MonoBehaviour
{
    private const float THROW_FORCE = 5f;
    public float direction;
    private Onomatopeya owner;

    private void Awake()
    {
        owner = GetComponent<Onomatopeya>();
    }

    private void FixedUpdate()
    {
        gameObject.transform.position += Vector3.right * THROW_FORCE * direction * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.tag == "Enemy")
        {
            owner.Active = true;
        }
    }
}
