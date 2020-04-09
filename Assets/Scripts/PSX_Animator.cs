using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSX_Animator : MonoBehaviour
{

    private bool snapWaiting = false;
    float waitTime = 0;
    float waitTimeTarget = 0;
    float normalizedSpeed = 1;



    private IEnumerator SnapAnimation(float speed)
    {

        //GetComponent<Animator>().Play(“Animation_Name”, 0, (1 / total_frames) * frame_number)

        yield return null;

    }

}
