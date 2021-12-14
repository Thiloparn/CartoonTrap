using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grap : IAction
{
    private GameObject grappedOno;

    public Grap()
    {
        this.grappedOno = null;
    }

    public override void ExecuteAction(PlayerController player)
    {

        if (player.pocket.IsEmpty())
        {
            player.pocket.SafeInPocket(grappedOno);
        }
        else
        {
            player.pocket.TakeOutOfPocket();
            grappedOno = null;
        }
    }

    public void SetGrappedOno(GameObject grappedOno)
    {
        this.grappedOno = grappedOno;
    }
}
