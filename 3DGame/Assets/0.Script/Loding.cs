using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Loding : Singleton<Loding>
{
    [SerializeField] private Slider loadingbar;
    [SerializeField] private TMP_Text loadingText;
    [SerializeField] private Transform loadingImage;

    public static string TargetScene { get; private set; } = string.Empty;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        TargetScene = sceneName;
        SceneManager.LoadScene("Loading");

    }
    private IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(TargetScene, LoadSceneMode.Additive);
        op.allowSceneActivation = false;

        while (op.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(op.progress);
            loadingbar.value = progress;
            loadingText.text = $"Loading... {(progress * 100f):F0}%";
            yield return null;
        }

        loadingbar.value = 1f;
        loadingText.text = $"Loading... 100%";
        // scene ON
        op.allowSceneActivation = true;
        // scene load finish
        while(!op.isDone)
            yield return null;

        // Å¸°Ù ¾ÀÀ» active·Î
        Scene target = SceneManager.GetSceneByName(TargetScene);
        SceneManager.SetActiveScene(target);

        yield return new WaitForSeconds(2f);

        //scene remove
        SceneManager.UnloadSceneAsync("Loading");
    }
}
