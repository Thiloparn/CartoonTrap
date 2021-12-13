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
        GameObject collidedGameObject = collision.gameObject;

        if (collidedGameObject.tag == "Punch")
        {
            ono.Active = true;
        }

        if (ono.Throwed && collidedGameObject.GetComponent<Collider2D>().isTrigger == false && collidedGameObject.tag != "Player" && collidedGameObject.tag != "Enemy")
        {
            ono.Active = true;
        }
    }
}
