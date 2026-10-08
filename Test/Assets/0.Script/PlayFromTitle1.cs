using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class PlayFromTitle
{

    static PlayFromTitle()
    {

        EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/9.Scenes/Test Firebase/TestFirebase.unity");

    }

}
