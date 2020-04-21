using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SceneFogController : MonoBehaviour
{

    public bool useColor = false;
    public bool useDistance = false;

    public Color fogColor;
    public float start = 25;
    public float end = 45;

    private void Update()
    {
        if (useColor) RenderSettings.fogColor = fogColor;

        if (useDistance)
        {
            RenderSettings.fogStartDistance = start;
            RenderSettings.fogEndDistance = end;
        }
    }

}
