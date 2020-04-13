using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash_PixelDough : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string wooshSound;

    [FMODUnity.EventRef]
    public string jingleSound;

    public void PlaySoundWoosh()
    {
        FMODUnity.RuntimeManager.PlayOneShot(wooshSound);
    }

    public void PlaySoundJingle()
    {
        FMODUnity.RuntimeManager.PlayOneShot(jingleSound);
    }

}
