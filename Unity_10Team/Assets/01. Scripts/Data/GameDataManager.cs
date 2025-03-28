using System.IO;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    private string savePath;

    void Start()
    {
        savePath = Application.persistentDataPath + "/playerSaveData.json";
    }

    // 🔹 데이터 저장
    public void SavePlayerData(PlayerSObj playerSObj)
    {
        PlayerSaveData data = new PlayerSaveData(playerSObj);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("데이터 저장 완료! 경로: " + savePath);
    }

    // 🔹 데이터 불러오기
    public PlayerSaveData LoadPlayerData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(json);
            Debug.Log($"불러오기 완료! 체력: {data.hp}, 공격력: {data.attack}");
            return data;
        }
        else
        {
            Debug.Log("저장된 데이터 없음");
            return null;
        }
    }
}
