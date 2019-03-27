using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerper : MonoBehaviour
{
    public float Speed = 1.0f;
    public Color StartColor;
    public Color EndColor;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        float t = (Time.deltaTime - startTime) * Speed;
        GetComponent<Renderer>().material.color = Color.Lerp(StartColor, EndColor, t);
    }
}
