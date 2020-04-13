using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DeathScreamController : MonoBehaviour
{

    public GameObject ghost;
    [FMODUnity.EventRef]
    public string screamSound;

    private CinemachineVirtualCamera vcam;

    private void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    public void PlayScene()
    {
        StartCoroutine(_PlayScene());
    }

    private IEnumerator _PlayScene()
    {

        CinemachineBrain brain = FindObjectOfType<CinemachineBrain>();

        brain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>().enabled = false;



        LTDescr tween = ghost.transform.LeanMoveLocalZ(-3, 3.2f);
        FMODUnity.RuntimeManager.PlayOneShotAttached(screamSound, ghost);

        while (LeanTween.isTweening(tween.id))
        {
            yield return null;
        }



        GameManager.Instance.ChangeScenes("Bedroom", "");

        yield return null;

    }

}
