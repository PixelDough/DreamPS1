using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public GameObject sceneChangePrefab;
    public string doorName = "DEFAULT";


    private void Awake()
    {
        transform.parent = null;
        //DontDestroyOnLoad(gameObject);
    }

    public void ChangeScenes(string _sceneName, string _doorName)
    {
        SceneChanger sceneChangeScript = Instantiate(sceneChangePrefab).GetComponent<SceneChanger>();

        sceneChangeScript.targetScene = _sceneName;
        sceneChangeScript.doorName = _doorName;
        doorName = _doorName;

    }

}
