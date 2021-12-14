using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPocket
{
    private GameObject onoInPocket;
    private PlayerController player;
    private Vector3 EXIT_POCKET_POSITION_OFFSET = new Vector3(0, 0.5f, 0);

    public PlayerPocket(PlayerController player)
    {
        this.player = player;
        onoInPocket = null;
    }

    public void SafeInPocket(GameObject ono)
    {
        if(ono != null)
        {
            Onomatopeya onoController = ono.GetComponent<Onomatopeya>();

            if (onoController != null && IsEmpty())
            {
                onoInPocket = ono;
                onoController.EnterPocket();
            }
        }
        
    }

    public void TakeOutOfPocket()
    {
        if (!IsEmpty())
        {
            onoInPocket.GetComponent<Onomatopeya>().ExitPocket(player.LookingAtDirection(), player.transform.position + EXIT_POCKET_POSITION_OFFSET);
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
