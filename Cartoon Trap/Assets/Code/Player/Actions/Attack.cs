using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IAction
{
    public void ExecuteAction(PlayerController player)
    {
        MonoBehaviour.print("Attack");
    }
}
