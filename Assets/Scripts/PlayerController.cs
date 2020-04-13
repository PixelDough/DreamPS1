using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerController : MonoBehaviour
{

    public GameObject model;
    public Animator animator;
    public float screamTimerTimeRemainingTarget = 5;
    public float screamTimerTimeMax = 5;
    public DeathScreamController deathCam;

    [FMODUnity.EventRef]
    public string footstepSoundEvent;

    private CharacterController cc;
    private Player p;

    private Vector3 sceneStartPosition;

    private float currentSpeedWalk = 0f;
    private float currentSpeedTurn = 0f;

    private Image thoughtBubble;
    private RectTransform screamTimer;
    private Image screamTimerMask;

    private RectTransform mainUI;

    private Interactable targetInteractable = null;

    private void Start()
    {
        cc = GetComponent<CharacterController>();

        List<SpawnPoint> points = new List<SpawnPoint>(FindObjectsOfType<SpawnPoint>());
        if (points.Count > 0)
        {
            foreach (SpawnPoint point in points)
            {
                Debug.Log(point.doorName);
                if (point.doorName == GameManager.Instance.doorName)
                {
                    cc.enabled = false;
                    transform.position = point.transform.position;
                    transform.rotation = point.transform.rotation;
                    cc.enabled = true;
                }
            }
        }

        sceneStartPosition = transform.position;

        p = ReInput.players.GetPlayer(0);

        thoughtBubble = GameObject.FindGameObjectWithTag("ThoughtBubble").GetComponent<Image>();

        screamTimer = GameObject.FindGameObjectWithTag("ScreamTimer").GetComponent<RectTransform>();
        screamTimerMask = screamTimer.GetComponentInChildren<Mask>().GetComponent<Image>();
        screamTimerTimeRemainingTarget = 0;

        mainUI = GameObject.FindGameObjectWithTag("MainUI").GetComponent<RectTransform>();
    }


    private void Update()
    {
        if (GameManager.Instance.playerCanMove)
        {
            bool isWalking = p.GetButton(RewiredConsts.Action.WalkForward);
            bool isTurning = p.GetAxis(RewiredConsts.Action.Turn) != 0;

            animator.SetBool("IsWalking", isWalking);

            if (isWalking)
            {
                currentSpeedWalk = Mathf.Lerp(currentSpeedWalk, 1, 4 * Time.deltaTime);
            }
            else
            {
                currentSpeedWalk = Mathf.Lerp(currentSpeedWalk, 0, 7 * Time.deltaTime);
            }

            if (isTurning)
            {
                currentSpeedTurn = Mathf.Lerp(currentSpeedTurn, 1 * Mathf.Sign(p.GetAxis(RewiredConsts.Action.Turn)), 3 * Time.deltaTime);
            }
            else
            {
                currentSpeedTurn = Mathf.Lerp(currentSpeedTurn, 0, 10 * Time.deltaTime);
            }

            transform.rotation = (Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y + currentSpeedTurn * 200f * Time.deltaTime, 0f)));

            Vector3 move = transform.forward * currentSpeedWalk * 5f;
            move += Physics.gravity;
            cc.Move(move * Time.deltaTime);

            if (targetInteractable != null)
            {
                if (p.GetButtonDown(RewiredConsts.Action.Interact))
                {
                    targetInteractable.Interact();
                }
            }

            if (transform.position.y < -10)
            {
                cc.enabled = false;
                transform.position = sceneStartPosition + Vector3.up * 10;
                cc.enabled = true;

                GameObject cam;
                cam = FindObjectOfType<Cinemachine.CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject;
                //cam.GetComponent<Cinemachine.CinemachineVirtualCamera>().OnTargetObjectWarped(transform, -(pos * 2));
                cam.GetComponent<Cinemachine.CinemachineVirtualCamera>().PreviousStateIsValid = false;
            }

            screamTimer.transform.localPosition = WorldToScreenSpace(transform.position + Vector3.up * 3f, Camera.main, mainUI);
            screamTimerMask.fillAmount = Mathf.MoveTowards(screamTimerMask.fillAmount, screamTimerTimeRemainingTarget, Time.deltaTime / screamTimerTimeMax);
            if (screamTimerMask.fillAmount < .25f) screamTimerMask.rectTransform.localPosition = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));

            if (screamTimerMask.fillAmount <= 0)
            {
                if (SceneManager.GetActiveScene().name == "Nightmare")
                {
                    animator.speed = 0;
                    GameManager.Instance.playerCanMove = false;

                    deathCam.PlayScene();

                }
            }
        }

    }


    public static Vector3 WorldToScreenSpace(Vector3 worldPos, Camera cam, RectTransform area)
    {
        Vector3 screenPoint = cam.WorldToScreenPoint(worldPos);
        screenPoint.z = 0;

        Vector2 screenPos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(area, screenPoint, cam, out screenPos))
        {
            return screenPos;
        }

        return screenPoint;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            float angle = Vector3.Angle(transform.eulerAngles.XZPlane(), other.transform.eulerAngles.XZPlane());

            targetInteractable = other.gameObject.GetComponent<Interactable>();
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Interactable")
        {
            thoughtBubble.enabled = true;

            thoughtBubble.transform.localPosition = WorldToScreenSpace(transform.position + Vector3.up * 2.5f, Camera.main, mainUI);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            thoughtBubble.enabled = false;

            // Remove interactable reference if you are leaving the current target interactable.
            if (targetInteractable == other.GetComponent<Interactable>())
            {
                targetInteractable = null;
            }
        }
    }

}
