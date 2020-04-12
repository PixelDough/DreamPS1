using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Lightswitch : Interactable
{

    [Header("Custom Interactable Variables")]
    [FMODUnity.ParamRef]
    public string lightStateParameter;
    public Light roomLight;

    private bool lightIsOn = true;


    public override void Interact()
    {
        base.Interact();

        lightIsOn = !lightIsOn;
        roomLight.enabled = lightIsOn;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(lightStateParameter, lightIsOn ? 1 : 0);

    }


}
