using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pum : IAction
{
    public override void ExecuteAction(PlayerController player)
    {
        positionOfInstanciation = player.transform.position;
        InstantiateOnomatopeya(player.pumOnomatopeya);
    }
}
