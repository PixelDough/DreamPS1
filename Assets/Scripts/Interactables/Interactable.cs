using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    [Header("Base Interactable Variables")]
    [FMODUnity.EventRef]
    public string interactSoundEvent;

    protected virtual void Awake()
    {
        gameObject.tag = "Interactable";
    }


    public virtual void Interact()
    {

        Debug.Log("Interacted with: " + gameObject.name);
        FMODUnity.RuntimeManager.PlayOneShotAttached(interactSoundEvent, gameObject);

    }

}
