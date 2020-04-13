using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Splash_FadeSpriteIn : MonoBehaviour
{

    private void OnEnable()
    {
        GameManager.Instance.ChangeScenes("TitleScreen", "");
        
    }

}
