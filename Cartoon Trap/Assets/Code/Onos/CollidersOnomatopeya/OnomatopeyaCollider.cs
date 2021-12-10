using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnomatopeyaCollider : MonoBehaviour
{
    protected Onomatopeya ono;

    private void Awake()
    {
        ono = GetComponent<Onomatopeya>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Punch")
        {
            ono.Active = true;
        }
    }
}