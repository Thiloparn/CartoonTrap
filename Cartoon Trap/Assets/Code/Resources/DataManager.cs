using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public PlayerController player;

    private void Awake()
    {
        GameData.player = player;
    }
}
