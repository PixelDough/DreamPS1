using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleBorder : MonoBehaviour
{

    public float innerRadius = 50f;
    public float outerRadius = 100f;
    
    private Image fadePanel;

    private PlayerController playerController;
    private float currentFadeNormalized = 0;
    private GameObject cam;
    private List<GhostController> ghosts;

    private bool isTeleporting = false;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        fadePanel = GameObject.FindGameObjectWithTag("FadeOverlay").gameObject.GetComponent<Image>();
        ghosts = new List<GhostController>(FindObjectsOfType<GhostController>());


    }


    private void LateUpdate()
    {

        //canFadeTime = Mathf.Max(canFadeTime - Time.deltaTime, 0f);

        float dist = Vector3.Distance(playerController.transform.position, transform.position);

        if (dist > outerRadius && !isTeleporting)
        {
            StartCoroutine(Teleport());
        }

        //Color c = fadePanel.color;
        //c.a = currentFadeNormalized;
        //fadePanel.color = c;

    }


    private IEnumerator Teleport()
    {
        isTeleporting = true;

        LTDescr tween = LeanTween.alpha(fadePanel.rectTransform, 1, 1f);
        while (LeanTween.isTweening(tween.id)) yield return null;

        // Teleport player
        Vector3 pos = new Vector3(playerController.transform.position.x, 0f, playerController.transform.position.z);
        float bigDist = Vector3.Distance(transform.position, playerController.transform.position);
        bigDist = Mathf.Clamp(bigDist, 0, outerRadius - 5);
        pos = pos.normalized;

        playerController.transform.position = -pos * bigDist;

        //// Teleport ghosts
        //foreach (GhostController ghost in ghosts)
        //{
        //    if (ghost.playerSeen || ghost.chasingPlayer)
        //    {
        //        ghost.agent.Warp(-pos + ghost.vectorToPlayer);
        //    }
        //}

        if (cam == null) { cam = FindObjectOfType<Cinemachine.CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject; }
        //cam.GetComponent<Cinemachine.CinemachineVirtualCamera>().OnTargetObjectWarped(transform, -(pos * 2));
        cam.GetComponent<Cinemachine.CinemachineVirtualCamera>().PreviousStateIsValid = false;

        cam.SetActive(false);
        yield return new WaitForEndOfFrame();
        cam.SetActive(true);

        tween = LeanTween.alpha(fadePanel.rectTransform, 0, 1f);
        while (LeanTween.isTweening(tween.id)) yield return null;

        isTeleporting = false;
        yield return null;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, innerRadius);
        Gizmos.DrawWireSphere(transform.position, outerRadius);
    }

}
