using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable_Door : Interactable
{

    [Header("Custom Interactable Variables")]
    public string targetSceneName;
    public bool isUnlocked = true;

    private SpawnPoint spawnPoint;


    private void Start()
    {
        spawnPoint = GetComponentInChildren<SpawnPoint>();
    }


    public override void Interact()
    {
        base.Interact();

        if (isUnlocked && Application.CanStreamedLevelBeLoaded(targetSceneName))
        {
            FindObjectOfType<GameManager>().ChangeScenes(targetSceneName, spawnPoint.doorName);
        }
    }


}
