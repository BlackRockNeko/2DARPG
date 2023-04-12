using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour {

    public GameObject loadingScreen;
    public Image loagingbar;

    public void loadlevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronusly(sceneIndex));
    }

    IEnumerator LoadAsynchronusly(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            loagingbar.fillAmount = progress;

            yield return null;
        }
    }

	}