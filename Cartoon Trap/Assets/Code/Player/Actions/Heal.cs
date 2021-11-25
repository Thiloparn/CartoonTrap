using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : IAction
{
    public void ExecuteAction(PlayerController player)
    {
        MonoBehaviour.print("Heal");
    }
}
