using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSave<T> where T : Object
{
    public static void SaveData(T data, string path = "savedata.json")
    {
        string filePath = Path.Combine(Application.persistentDataPath, path);
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, jsonData);
    }

    public static T LoadData(string path = "savedata.json")
    {
        string filePath = Path.Combine(Application.persistentDataPath, path);

        if (!File.Exists(filePath))
            return null;

        string jsonData = File.ReadAllText(filePath);
        return JsonUtility.FromJson<T>(jsonData);
    }
}
