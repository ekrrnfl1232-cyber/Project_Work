using UnityEngine;

public class Door : MonoBehaviour, IInterectable
{
    [SerializeField] private SceneType nextScene;
    public void Interact()
    {
        SceneLoader.Instance.LoadingScene(nextScene);
    }
    
}
