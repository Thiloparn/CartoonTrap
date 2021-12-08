using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : IAction
{
    public void ExecuteAction(PlayerController player)
    {
        MonoBehaviour.print("Jump");
    }
}
