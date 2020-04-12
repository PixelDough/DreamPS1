using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempFootstepController : MonoBehaviour
{

    PlayerController player;

    private string footstepSoundEvent;

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();

        footstepSoundEvent = player.footstepSoundEvent;
    }


    public void PlayFootstepSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(footstepSoundEvent, gameObject);
    }

}
