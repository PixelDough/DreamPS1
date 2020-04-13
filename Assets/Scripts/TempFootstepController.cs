using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempFootstepController : MonoBehaviour
{

    private SceneAttributes sceneAttributes;

    private void Start()
    {
        sceneAttributes = FindObjectOfType<SceneAttributes>();
    }


    public void PlayFootstepSound()
    {
        if (sceneAttributes.footstepSound != "") FMODUnity.RuntimeManager.PlayOneShotAttached(sceneAttributes.footstepSound, gameObject);
    }

}
