using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampPost : MonoBehaviour
{

    SphereCollider collider;
    PlayerController player;


    private void Start()
    {
        collider = GetComponent<SphereCollider>();
        player = FindObjectOfType<PlayerController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.screamTimerTimeRemainingTarget = player.screamTimerTimeMax;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.screamTimerTimeRemainingTarget = 0;
        }
    }

}
