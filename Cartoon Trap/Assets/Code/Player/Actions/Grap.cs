using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grap : IAction
{
    public override void ExecuteAction(PlayerController player)
    {
        MonoBehaviour.print("Grap");
    }
}
