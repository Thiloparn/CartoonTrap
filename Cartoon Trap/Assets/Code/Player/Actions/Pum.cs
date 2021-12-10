using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pum : IAction
{
    public override void ExecuteAction(PlayerController player)
    {
        InstantiateOnomatopeya(player.pumOnomatopeya, player);
        player.playerAnimator.StartUsingHammerAnimation(player);
    }
}
