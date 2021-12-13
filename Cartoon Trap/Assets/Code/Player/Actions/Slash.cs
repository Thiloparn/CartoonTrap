using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : IAction
{
    public override void ExecuteAction(PlayerController player)
    {
        InstantiateOnomatopeya(player.slashOnomatpeya, player);
        onoInstanciated.GetComponent<SlashOnomatopeya>().slashDirection = player.MovingDirectionX;
    }
}
