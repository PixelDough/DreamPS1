using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public GameObject model;
    public Animator animator;

    [FMODUnity.EventRef]
    public string footstepSoundEvent;

    private CharacterController cc;
    private Player p;

    private float currentSpeedWalk = 0f;
    private float currentSpeedTurn = 0f;

    private Image thoughtBubble;

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

        
        p = ReInput.players.GetPlayer(0);

        thoughtBubble = GameObject.FindGameObjectWithTag("ThoughtBubble").GetComponent<Image>();
        mainUI = GameObject.FindGameObjectWithTag("MainUI").GetComponent<RectTransform>();
    }


    private void Update()
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
