using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public class SavesSystem : MonoBehaviour
{
    // Carga en un diccionario todas las variables importantes del juego
    Dictionary<string, string> populateData()
    {

        Dictionary<string, string> data = new Dictionary<string, string>();

        //En este método se debe llenar la variable data con todos los datos que nos hacen falta, además de la fecha en la que se hace el guardado

        data.Add("date", DateTime.Now.ToString());

        return data;
    }

    //Guarda el diccionario de todas las variables importantes del juego
    void saveData(string name, Dictionary<string, string> data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + name + ".fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

        //Debug.Log("Partida guardada en "+ path);
    }

    //Carga en un diccionario todas las variables importantes del juego guardadas en un fichero de guardado
    Dictionary<string, string> loadData(string name)
    {
        Dictionary<string, string> res = new Dictionary<string, string>();
        string path = Application.persistentDataPath + "/" + name + ".fun";

        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            res = (Dictionary<string, string>)formatter.Deserialize(stream);
            stream.Close();
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }

        return res;
    }

    //Utiliza el diccionario con todas las variables importates del juego para modificar la partida actual
    void applyData(Dictionary<string, string> data)
    {
        //Aqui se deben cargar los datos necesarios para acceder a la partida

        //Debug.Log("Partida cargada: "+ data["date"]);
    }

    //Guarda la partida bajo el nombre elegido
    public void saveGame(string gameName)
    {
        Dictionary<string, string>  data = populateData();

        saveData(gameName, data);
    }

    //Carga la partida con el nombre elegido
    public void loadGame(string gameName)
    {
        Dictionary<string, string> data =  loadData(gameName);

        if(data.Count != 0)
        {
            applyData(data);
        }
    }

    //Borra la partida con el nombre elegido
    public void deleteGame(string gameName)
    {
        string path = Application.persistentDataPath + "/" + gameName + ".fun";

        if (File.Exists(path))
        {
            File.Delete(path);
            //Debug.Log("Partida borrada");
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }
}
