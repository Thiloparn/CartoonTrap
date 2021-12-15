using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeOnomatopeyaCollider : OnomatopeyaCollider
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedGameObject = collision.gameObject;

        if (collidedGameObject.tag == "Player")
        {
            if(ono != null)
            {
                ono.Active = true;
            }
        }

        if (ono.Throwed && collidedGameObject.GetComponent<Collider2D>().isTrigger == false && collidedGameObject.tag != "Enemy")
        {
            ono.Active = true;
        }
    }
}
