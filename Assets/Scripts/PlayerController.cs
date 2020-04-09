using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{

    public GameObject model;
    public Animator animator;

    private CharacterController cc;
    private Player p;

    private float currentSpeedWalk = 0f;
    private float currentSpeedTurn = 0f;


    private void Start()
    {
        cc = GetComponent<CharacterController>();
        p = ReInput.players.GetPlayer(0);
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

        transform.rotation = (Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y + currentSpeedTurn * 4f, 0f)));

        Vector3 move = transform.forward * currentSpeedWalk * 5f;
        move += Physics.gravity;
        cc.Move(move * Time.deltaTime);
    }

}
