using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashOnomatopeya : Onomatopeya
{
    public float slashDirection = 1f;

    public override void ExitPocket(float direction, Vector3 position)
    {
        base.ExitPocket(direction, position);
        slashDirection = direction;
    }
}
