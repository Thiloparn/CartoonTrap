using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchActivator : MechanicActivator
{
    protected override void ActivateMechanic(PlayerController playerController)
    {
        playerController.UnlockPunch();
    }
}
