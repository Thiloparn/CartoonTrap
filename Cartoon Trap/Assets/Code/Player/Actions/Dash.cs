using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : IAction
{
    public void ExecuteAction(PlayerController player)
    {
        MonoBehaviour.print("Dash");
    }
}
