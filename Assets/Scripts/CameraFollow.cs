using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Target;
    public float LerpSpeed = 3;

    private Vector3 startOffset = Vector3.zero;

    void Awake()
    {
        startOffset = transform.position - Target.transform.position;
    }

    void Update()
    {
        Vector3 offset = transform.position - Target.transform.position - startOffset;
        transform.position = Vector3.Lerp(transform.position, transform.position - offset, Time.deltaTime * LerpSpeed);
    }
}
