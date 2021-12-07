using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeOnomatopeyaCollider : OnomatopeyaCollider
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ono.Active = true;
        }
    }
}