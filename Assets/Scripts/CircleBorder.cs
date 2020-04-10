using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleBorder : MonoBehaviour
{

    public float innerRadius = 50f;
    public float outerRadius = 100f;
    public Image fadePanel;

    private PlayerController playerController;
    private float currentFadeNormalized = 0;
    private GameObject cam;


    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }


    private void LateUpdate()
    {

        float dist = Vector3.Distance(playerController.transform.position, transform.position);
        
        if (dist < innerRadius)
        {
            currentFadeNormalized = 0;
        }

        if (dist >= innerRadius && dist <= outerRadius) currentFadeNormalized = ((dist - innerRadius) / (outerRadius - innerRadius));
        
        if (dist > outerRadius)
        {
            if (cam == null) { cam = FindObjectOfType<Cinemachine.CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject; }

            currentFadeNormalized = 1;
            Vector3 pos = new Vector3(playerController.transform.position.x, 0f, playerController.transform.position.z);
            Vector3.ClampMagnitude(pos, outerRadius - 1);
            playerController.transform.position = -pos;

            cam.GetComponent<Cinemachine.CinemachineVirtualCamera>().OnTargetObjectWarped(transform, -(pos * 2));
            cam.GetComponent<Cinemachine.CinemachineVirtualCamera>().PreviousStateIsValid = false;

            //cam.SetActive(false);
            //cam.SetActive(true);
        }

        Color c = fadePanel.color;
        c.a = currentFadeNormalized;
        fadePanel.color = c;

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, innerRadius);
        Gizmos.DrawWireSphere(transform.position, outerRadius);
    }

}
