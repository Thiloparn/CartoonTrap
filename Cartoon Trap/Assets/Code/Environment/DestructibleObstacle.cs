using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print(collision.gameObject.tag);

        if (collision.gameObject.tag == "PumEffectArea")
        {
            Destroy(gameObject);
        }
    }
}
