using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IAction
{
    private Transform transform;

    public Attack(Transform transform)
    {
        this.transform = transform;
    }

    public void ExecuteAction(PlayerController player)
    {
        MonoBehaviour.Instantiate<GameObject>(player.attackOnomatopeya, transform.position, Quaternion.identity);
    }
}
