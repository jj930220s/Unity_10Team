using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSave<T>
{
    public static void SaveData(T data, string path = "savedata.json")
    {
        string filePath = Path.Combine(Application.persistentDataPath, path);

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, jsonData);

        Debug.Log("file saved in " + filePath);
    }

    public static T LoadData(string path = "savedata.json")
    {
        string filePath = Path.Combine(Application.persistentDataPath, path);

        if (!File.Exists(filePath))
        {
            Debug.Log("file doesn't exist. return default");
            return default;
        }

        string jsonData = File.ReadAllText(filePath);
        T data = JsonUtility.FromJson<T>(jsonData);
        Debug.Log("file loaded succesfully");
        return data;
    }
}
