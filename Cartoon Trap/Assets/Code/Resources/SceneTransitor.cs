using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitor : MonoBehaviour
{
    public string sceneName;
    public Vector3 aparitionPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameData.playerAparitionPosition = aparitionPosition;
            SceneManager.LoadScene(sceneName);
        }
    }
}
