using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : IAction
{
    public override void ExecuteAction(PlayerController player)
    {
        MonoBehaviour.print("Slash");
    }
}
