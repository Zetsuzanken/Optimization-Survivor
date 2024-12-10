using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Candle : MonoBehaviour
{
    private Light light;
    private float randIndex;

    void Start()
    {
        light = GetComponentInChildren<Light>();
        randIndex = Random.Range(0, 1000f);
    }

    void Update()
    {
        light.intensity = Mathf.PerlinNoise1D(Time.time * 4f + randIndex) + 1.0f;
    }
}
