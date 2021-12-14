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
        
        //Booleans
        data.Add("finalDoorOpen", GameData.finalDoorOpen.ToString());
        data.Add("r", GameData.r.ToString());
        data.Add("g", GameData.g.ToString());
        data.Add("b", GameData.b.ToString());
        data.Add("punchLocked", GameData.punchLocked.ToString());
        data.Add("slashLocked", GameData.slashLocked.ToString());
        data.Add("pumLocked", GameData.pumLocked.ToString());
        data.Add("phiuLocked", GameData.phiuLocked.ToString());
        data.Add("hopLocked", GameData.hopLocked.ToString());

        //Integers
        data.Add("maxPlayerHealt", GameData.maxPlayerHealt.ToString());
        data.Add("currentPlayerHealth", GameData.currentPlayerHealth.ToString());
        data.Add("coins", GameData.coins.ToString());
        data.Add("attackPower", GameData.attackPower.ToString());

        //Vector3
        SafeVector3InDictionary(GameData.lastRestZone, "lastRestZone", data);

        //Dates
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
        DataManager dataManager = GetComponent<DataManager>();

        //Boolean
        GameData.finalDoorOpen = StringToBool(data["finalDoorOpen"]);
        GameData.finalDoorOpen = StringToBool(data["r"]);
        GameData.finalDoorOpen = StringToBool(data["g"]);
        GameData.finalDoorOpen = StringToBool(data["b"]);
        GameData.finalDoorOpen = StringToBool(data["punchLocked"]);
        GameData.finalDoorOpen = StringToBool(data["slashLocked"]);
        GameData.finalDoorOpen = StringToBool(data["pumLocked "]);
        GameData.finalDoorOpen = StringToBool(data["phiuLocked"]);
        GameData.finalDoorOpen = StringToBool(data["hopLocked "]);

        //Integer
        GameData.maxPlayerHealt = StringToInt(data["maxPlayerHealt"]);
        GameData.currentPlayerHealth = StringToInt(data["currentPlayerHealth"]);
        GameData.coins = StringToInt(data["coins"]);
        GameData.attackPower = StringToInt(data["attackPower"]);

        //Vector3
        GameData.lastRestZone = StringToVector3("lastRestZone", data);

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

    private bool StringToBool(string boolName)
    {
        if(boolName == true.ToString())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int StringToInt(string intName)
    {
        return int.Parse(intName);
    }

    private Vector3 StringToVector3(string vectorName, Dictionary<string, string> dictionary)
    {
        Vector3 returned = Vector3.zero;

        returned.x = StringToInt(dictionary[vectorName + "X"]);
        returned.y = StringToInt(dictionary[vectorName + "Y"]);
        returned.z = StringToInt(dictionary[vectorName + "Z"]);

        return returned;
    }

    private void SafeVector3InDictionary(Vector3 vector, string vectorName, Dictionary<string, string> dictionary)
    {
        dictionary.Add(vectorName + "X", vector.x.ToString());
        dictionary.Add(vectorName + "Y", vector.y.ToString());
        dictionary.Add(vectorName + "Z", vector.z.ToString());
    }
}
