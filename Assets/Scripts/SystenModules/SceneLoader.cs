using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 场景加载器类
/// </summary>
public class SceneLoader : PersistentSingleton<SceneLoader>
{
    [SerializeField] Image transitionImage;
    [SerializeField] float fadeTime = 3.5f;
    Color color;
    const string GAME_PLAY = "Game";
    IEnumerator LoadCoroutine(string sceneName)
    {
        var loadingOperation=SceneManager.LoadSceneAsync(sceneName);
        loadingOperation.allowSceneActivation = false;

        transitionImage.gameObject.SetActive(true);
        //淡出效果
        while (color.a < 1f)
        {
            color.a=Mathf.Clamp01(color.a + Time.unscaledDeltaTime / fadeTime) ;
            transitionImage.color = color;
            yield return null;
        }
        loadingOperation.allowSceneActivation = true;
        //淡入效果
        while (color.a > 0f)
        {
            color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / fadeTime);
            transitionImage.color = color;
            yield return null;
        }
        transitionImage.gameObject.SetActive(false);
    }
    public void LoadGameplayScene()
    {
        StartCoroutine(LoadCoroutine(GAME_PLAY));
    }
}
