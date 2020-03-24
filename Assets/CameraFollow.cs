
using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float smoothSpeed = 0f;
    public Vector3 _startingOffset;
    public Vector3 zoom;
    public int zoomSpeed;
    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + _startingOffset + zoom;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private void Update()
    {
        InputHandler();
    }

    private void InputHandler()
    {
        float mouseDelta = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        zoom.z += mouseDelta;
        zoom.y += -mouseDelta / 5;//* (zoom.x/10)
    }
}
