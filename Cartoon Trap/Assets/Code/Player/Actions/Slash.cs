using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : IAction
{
    public override void ExecuteAction(PlayerController player)
    {
        positionOfInstanciation = player.transform.position;
        InstantiateOnomatopeya(player.slashOnomatpeya);
        onoInstanciated.GetComponent<SlashOnomatopeya>().slashDirection = player.MovingDirectionX;
    }
}
