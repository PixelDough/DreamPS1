using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public string doorName;
    public string targetScene;
    public CanvasGroup loadScreen;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(_ChangeScenes());
    }


    private IEnumerator _ChangeScenes()
    {

        LTDescr tweenIn = LeanTween.alphaCanvas(loadScreen, 1, 1f);

        while(LeanTween.isTweening(tweenIn.id))
        {
            yield return null;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        LTDescr tweenOut = LeanTween.alphaCanvas(loadScreen, 0, 1f);

        while (LeanTween.isTweening(tweenOut.id))
        {
            yield return null;
        }

        Destroy(gameObject);

        yield return null;
    }

}
