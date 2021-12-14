using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public GameObject playerprefab;
    

    private void Awake()
    {
        Instantiate(playerprefab);
    }
    

}
