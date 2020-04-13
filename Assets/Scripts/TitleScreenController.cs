using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class TitleScreenController : MonoBehaviour
{

    public GameObject title;
    public Animator playerAnimator;
    public Animator ghostAnimator;

    Player p;

    private void Start()
    {
        title.transform.LeanMoveLocal(new Vector3(0f, 0f, 10f), 1.5f).setEaseOutElastic();

        playerAnimator.SetBool("IsWalking", true);
        ghostAnimator.Play("Chase");

        p = ReInput.players.GetPlayer(0);
    }


    private void Update()
    {
        if (p.GetButtonDown(RewiredConsts.Action.Start))
        {
            GameManager.Instance.ChangeScenes("Bedroom", "");
        }
    }

}
