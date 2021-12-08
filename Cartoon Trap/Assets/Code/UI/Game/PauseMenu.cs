using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button botonContinuar;
    public Button botonOpciones;
    public Button botonSalir;
    [SerializeField] GameObject pauseMenu;
    bool gameIsPaused;

    private void Awake()
    {
        botonContinuar.onClick.AddListener(Continuar);
        botonOpciones.onClick.AddListener(OptionsEnter);
        botonSalir.onClick.AddListener(Salir);
        gameIsPaused = false;
        pauseMenu.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (gameIsPaused)
            {
                Continuar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Pausar()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void Continuar()
    {
        pauseMenu.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1f;

    }

    public void OptionsEnter()
    {
        //Código para entrar en opciones
    }

    public void OptionsExit()
    {
        //Código para volver al menu principal
    }

    public void OptionsSaveExit()
    {
        //Código para volver al menu principal guardando las opciones
    }

    public void Salir()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}