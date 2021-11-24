using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button botonNuevaPartida;
    public Button botonCargarPartida;
    public Button botonOpciones;
    public Button botonSalir;

    private void Awake()
    {
        botonNuevaPartida.onClick.AddListener(NewGame);
        botonCargarPartida.onClick.AddListener(LoadGame);
        botonOpciones.onClick.AddListener(OptionsEnter);
        botonSalir.onClick.AddListener(Salir);
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        //Habrá que editar esto para que se cargue con una partida específica
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
        Application.Quit();
    }
}
