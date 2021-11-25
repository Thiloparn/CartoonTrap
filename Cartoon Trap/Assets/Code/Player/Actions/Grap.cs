using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grap : IAction
{
    public void ExecuteAction(PlayerController player)
    {
        MonoBehaviour.print("Grap");
    }
}
