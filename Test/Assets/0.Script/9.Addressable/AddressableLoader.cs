using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class AddressableLoader : MonoBehaviour
{
    private AsyncOperationHandle<GameObject> handle;
    private GameObject monster;

    private void Start()
    {
        LoadEnemy("Skeleton");
    }
    
    private void LoadEnemy(string name)
    {
        handle = Addressables.LoadAssetAsync<GameObject>($"{name}");
        handle.Completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperationHandle<GameObject> operation)
    {
        if (operation.Status != AsyncOperationStatus.Succeeded)
            return;
        GameObject prefab = operation.Result;
        Debug.Log("로드 성공?");
    }

    private void OnDestroy()
    {
        if(handle.IsValid())
        {
            Addressables.Release(handle);
        }
    }
}
