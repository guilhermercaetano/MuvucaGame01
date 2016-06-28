using UnityEngine;
using System.Collections;
using System;

public class FadeAndClear : MonoBehaviour
{
    private GUITexture _GUITexture;

    public bool SceneClear = false;
    public bool SceneFade = false;
                                 
    public float ScreenFaderSpeed = 2;

    public float LastValue = 1f;
    public float t = 0f;
    
    void Awake()
    {
        _GUITexture = GetComponent<GUITexture>();
        _GUITexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
        _GUITexture.color = Color.black;
        SceneClear = true;
    }

    void FixedUpdate()
    {
        if (SceneClear)
        {
            t += Time.deltaTime * ScreenFaderSpeed;
            float Value = (float)(0.84 * (t * t) + 0.88* t + 1);
            float Increment = Value - LastValue;
            LastValue = Value;
            
            _GUITexture.color = Color.LerpUnclamped(_GUITexture.color, Color.clear,
                                                    Increment);
            if (_GUITexture.color.a < 0.01f)
            {
                _GUITexture.color = Color.clear;
                SceneClear = false;
                t = 0f;
                LastValue = 1f;
            }
        }

        if (SceneFade)
        {
            t += (Time.deltaTime * ScreenFaderSpeed)/2;
            float Value = (float)(0.84 * (t * t) + 0.88* t + 1);
            float Increment = Value - LastValue;
            LastValue = Value;

            _GUITexture.color = Color.Lerp(_GUITexture.color, Color.black,
                                           Increment);
            
            if ((_GUITexture.color.a > 0.99f))
            {
                _GUITexture.color = Color.black;
                SceneFade = false;
                t = 0f;
                LastValue = 1f;
            }
        }
    }
}
