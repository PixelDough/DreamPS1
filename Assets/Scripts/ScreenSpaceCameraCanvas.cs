using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSpaceCameraCanvas : MonoBehaviour
{

    private void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas)
        {
            canvas.worldCamera = Camera.main;
        }
    }

}
