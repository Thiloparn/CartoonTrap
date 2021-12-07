using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPocket
{
    private GameObject onoInPocket;
    private PlayerController player;

    public PlayerPocket(PlayerController player)
    {
        this.player = player;
        onoInPocket = null;
    }

    public void SafeInPocket(GameObject ono)
    {
        Onomatopeya onoController = ono.GetComponent<Onomatopeya>();

        if(onoController != null && IsEmpty())
        {
            onoInPocket = ono;
            onoController.EnterPocket();
        }
    }

    public void TakeOutOfPocket()
    {
        if (!IsEmpty())
        {
            onoInPocket.GetComponent<Onomatopeya>().ExitPocket(player.LookingAtDirection(), player.transform.position);
            onoInPocket = null;
        }
    }

    public bool IsEmpty()
    {
        if(onoInPocket == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
