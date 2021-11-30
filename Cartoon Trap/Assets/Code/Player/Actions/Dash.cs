using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : IAction
{
    public override void ExecuteAction(PlayerController player)
    {
        MonoBehaviour.print("Dash");
    }
}
