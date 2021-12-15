using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopActivator : MechanicActivator
{
    protected override void ActivateMechanic(PlayerController playerController)
    {
        playerController.UnlockHop();
    }
}
