using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pum : IAction
{
    public void ExecuteAction(PlayerController player)
    {
        MonoBehaviour.print("Pum");
    }
}
