using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhiuActivator : MechanicActivator
{
    protected override void ActivateMechanic(PlayerController playerController)
    {
        playerController.UnlockPhiu();
    }
}
