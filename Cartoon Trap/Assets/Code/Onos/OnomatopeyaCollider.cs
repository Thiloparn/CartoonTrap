using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnomatopeyaCollider : MonoBehaviour
{
    private Onomatopeya ono;

    private void Awake()
    {
        ono = GetComponent<Onomatopeya>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Punch")
        {
            print("Activación");
            ono.Active = true;
        }
    }
}
