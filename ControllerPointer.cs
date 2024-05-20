using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPointer : MonoBehaviour
{
    public OVRInput.Controller controller;
    public LineRenderer lineRenderer;
    public float offsetHeight = 0.0f;  // Offset in meters

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        Vector3 controllerPosition = transform.position;
        Quaternion controllerRotation = transform.rotation;

        // Offset the start position upward by the specified height
        Vector3 startPosition = controllerPosition;

        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, startPosition + controllerRotation * Vector3.down * 3);
    }
}
