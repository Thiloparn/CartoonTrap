using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public GameObject playerprefab;
    public Vector3 spawn = new Vector3(0, 0, 0);
    public Cinemachine.CinemachineVirtualCamera mainCamera;
    

    private void Awake()
    {
        GameObject player = Instantiate(playerprefab);
        mainCamera.Follow = player.transform;
        player.transform.position = spawn;
        //player.transform.position = GameData.playerAparitionPosition;
    }
    

}
