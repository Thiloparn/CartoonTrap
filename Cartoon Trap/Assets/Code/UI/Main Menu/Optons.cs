using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Optons : MonoBehaviour
{
    public GameObject mainMenuCanvas;

    private void Awake()
    {
        
    }

    public void BackToMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
