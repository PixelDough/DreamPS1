using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable_Door : Interactable
{

    [Header("Custom Interactable Variables")]
    [FMODUnity.EventRef]
    public string lockedSoundEvent;
    public string targetSceneName;
    public GameObject lockObject;
    public bool isUnlocked = true;

    private SpawnPoint spawnPoint;


    private void Start()
    {
        spawnPoint = GetComponentInChildren<SpawnPoint>();

        if (lockObject) lockObject.SetActive(!isUnlocked);
    }


    public override void Interact()
    {
        string tempSound = interactSoundEvent;
        if (!isUnlocked) interactSoundEvent = lockedSoundEvent;

        base.Interact();

        interactSoundEvent = tempSound;

        if (isUnlocked && Application.CanStreamedLevelBeLoaded(targetSceneName))
        {
            GameManager.Instance.ChangeScenes(targetSceneName, spawnPoint.doorName);
        }
    }


}
