using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashActivator : MechanicActivator
{
    protected override void ActivateMechanic(PlayerController playerController)
    {
        playerController.UnlockSlash();
    }
}
