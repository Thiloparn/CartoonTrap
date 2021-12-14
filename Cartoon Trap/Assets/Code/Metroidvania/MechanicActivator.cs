using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicActivator : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ActivateMechanic(collision.gameObject.GetComponent<PlayerController>());
        }
    }

    protected virtual void ActivateMechanic(PlayerController playerController)
    {
        throw new NotImplementedException();
    }
}
