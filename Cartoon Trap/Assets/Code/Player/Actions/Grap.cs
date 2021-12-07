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
            MonoBehaviour.print("Grap");
            player.pocket.SafeInPocket(grappedOno);
        }
        else
        {
            MonoBehaviour.print("Throw");
            player.pocket.TakeOutOfPocket();
            grappedOno = null;
        }
        
    }

    public void SetGrappedOno(GameObject grappedOno)
    {
        this.grappedOno = grappedOno;
    }
}
