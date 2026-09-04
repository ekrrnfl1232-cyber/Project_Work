using UnityEngine;

public class Testjson : MonoBehaviour
{

    void Start()
    {
        TestSaveData data = new TestSaveData();
        data.playerName = "Hero";
        data.level = 1;
        data.gold = 200;
        data.exp = 50f;

        // 저장 // 보안코드
        string jsonDataString = JsonUtility.ToJson(data, true);
        Debug.Log(jsonDataString);
        // 로드
        TestSaveData loadData = JsonUtility.FromJson<TestSaveData>(jsonDataString);

        Debug.Log(Application.persistentDataPath);
    }

}
