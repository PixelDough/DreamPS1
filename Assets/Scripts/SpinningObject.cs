using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObject : MonoBehaviour
{

    public Vector3 anglePerSecond;

    private void Update()
    {
        transform.eulerAngles = transform.eulerAngles + (anglePerSecond * Time.deltaTime);
    }

}
