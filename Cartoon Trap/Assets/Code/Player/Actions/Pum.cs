using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABCD : IAction
{
    public void ExecuteAction(PlayerController player)
    {
        MonoBehaviour.Instantiate(player.pumOnomatopeya, player.transform.position, Quaternion.identity);
    }
}
