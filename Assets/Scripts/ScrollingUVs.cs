﻿using UnityEngine;
using System.Collections;

public class ScrollingUVs : MonoBehaviour
{
    public int materialIndex = 0;
    public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);
    public string textureName = "_MainTex";

    Vector2 uvOffset = Vector2.zero;

    new Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void LateUpdate()
    {
        uvOffset += (uvAnimationRate * Time.deltaTime);
        if (renderer.enabled)
        {
            renderer.materials[materialIndex].SetTextureOffset(textureName, uvOffset);
        }
    }
}