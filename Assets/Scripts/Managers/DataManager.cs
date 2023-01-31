using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class DataManager : MonoBehaviour
{
    public LevelStruct[] levels;
    public int maxUnlockedLevel = 1;

    private string savePath;
    private string saveFileName = "data.json";
    public static DataManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
#if UNITY_ANDROID && !UNITY_EDITOR
        savePath = Path.Combine(Application.persistentDataPath, saveFileName);     
#else
        savePath = Path.Combine(Application.dataPath, saveFileName);
#endif
    }
    public void SaveToFile()
    {
        PlayerDataStruct playerData = new PlayerDataStruct
        {
            maxUnlockedLevel = this.maxUnlockedLevel,
            levels = this.levels
        };
        string json = JsonUtility.ToJson(playerData, prettyPrint: true);
        try
        {
            File.WriteAllText(savePath, json);
        }
        catch(System.Exception)
        {
            Debug.Log("Error when save data");
        }
    }
    public void LoadFromFile()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("File doesn't exist");
            return;
        }
        try
        {
            string json = File.ReadAllText(savePath);
            PlayerDataStruct playerDataFromJson = JsonUtility.FromJson<PlayerDataStruct>(json);
            maxUnlockedLevel = playerDataFromJson.maxUnlockedLevel;
            levels = playerDataFromJson.levels;
        }
        catch (System.Exception)
        {

            Debug.Log("Error when loading data");
        }
    }
}

[System.Serializable]
public struct LevelStruct
{
    public int index;
    public int maxScore;
    public int stars;
}

[System.Serializable]
public struct PlayerDataStruct
{
    public LevelStruct[] levels;
    public int maxUnlockedLevel;
}