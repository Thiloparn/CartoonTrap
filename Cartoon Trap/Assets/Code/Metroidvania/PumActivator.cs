using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumActivator : MechanicActivator
{
    protected override void ActivateMechanic(PlayerController playerController)
    {
        playerController.UnlockPum();
    }
}
