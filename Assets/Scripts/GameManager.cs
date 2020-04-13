using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public GameObject sceneChangePrefab;
    public string doorName = "DEFAULT";
    public bool playerCanMove = true;


    private void Awake()
    {
        transform.parent = null;
        //DontDestroyOnLoad(gameObject);

        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ScreenCapture.CaptureScreenshot("D:/Unity Projects/DreamPS1/Assets/Screenshots/DreamPS1_Screenshot_" + System.DateTime.Now.Ticks + ".png");
        }
    }

    public void ChangeScenes(string _sceneName, string _doorName)
    {
        SceneChanger sceneChangeScript = Instantiate(sceneChangePrefab).GetComponent<SceneChanger>();

        sceneChangeScript.targetScene = _sceneName;
        sceneChangeScript.doorName = _doorName;
        doorName = _doorName;

    }

}
