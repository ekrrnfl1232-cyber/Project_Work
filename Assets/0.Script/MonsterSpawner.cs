using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Monster monster1;
    public Transform monster2;
    public Transform parent;
    private void Awake()
    {

    }
    void Start()
    {
        Instantiate(monster2, parent);
        Instantiate(monster2, parent);
        Instantiate(monster2, parent);
        Instantiate(monster2, parent);
        Instantiate(monster2, parent);
        Instantiate(monster2);
        Destroy(monster2);
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //물리 업데이트
    }

    private void LateUpdate()
    {
        
    }
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void OnValidate()
    {
        
    }

    private void OnApplicationQuit()
    {
        
    }

    private void OnApplicationPause(bool pause)
    {
        Application.Quit();
    }
}




