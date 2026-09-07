using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSaveData
{
    public int Hp;
    public int level;
    public float exp;
    public int gold;
    public InvenData[] invendata;
    public EquipData[] equipDatas;
}
[System.Serializable]
public class InvenData
{
    public int id;
    public uint stack;
}
[System.Serializable]
public class EquipData
{
    public int id;
}

public class SaveManager : Singleton<SaveManager>
{
    public string savePath;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        savePath = Path.Combine(Application.persistentDataPath, "save.Json");
    }
    public void Save(GameSaveData data)
    {
        if (data == null)
        {
            Debug.Log("데이터 누락");
            return;
        }
        string json = JsonUtility.ToJson(data, true);
        Debug.Log(json);
        File.WriteAllText(savePath, json);
    }
    [ContextMenu("Load")]
    public GameSaveData Load()
    {
        if(!File.Exists(savePath))
        {
            Debug.Log("Save File 누락");
            return null;
        }
        try
        {
            string json = File.ReadAllText(savePath);
            GameSaveData data = JsonUtility.FromJson<GameSaveData>(json);
            return data;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
    }
    [ContextMenu("Delete")]
    public void Delete()
    {
        if(!File.Exists(savePath))
        {
            return;
        }

        Debug.Log($"{savePath} 삭제");
        File.Delete(savePath);
    }
}
