using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 100.0f;
    public float zoomSpeed = 10.0f;
    public float minZoom = 2.0f;
    public float maxZoom = 20.0f;
    public float panSpeed = 0.1f;
    public float panSmoothing = 10.0f; // New parameter for smoothing

    // Boundaries for camera movement
    public Vector2 panLimitMin = new Vector2(-5, -5);
    public Vector2 panLimitMax = new Vector2(5, 5);

    private Vector3 lastMousePosition;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        // Rotate camera with right mouse button
        if (Input.GetMouseButton(1))
        {
            float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float verticalRotation = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            transform.RotateAround(Vector3.zero, Vector3.up, horizontalRotation);
            transform.RotateAround(Vector3.zero, transform.right, -verticalRotation);
        }

        // Zoom camera with scroll wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        Camera.main.orthographicSize -= scrollInput;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);

        // Pan camera with middle mouse button
        if (Input.GetMouseButtonDown(2))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(2))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            Vector3 translation = new Vector3(-delta.x * panSpeed, -delta.y * panSpeed, 0);
            targetPosition += translation;
            targetPosition.x = Mathf.Clamp(targetPosition.x, panLimitMin.x, panLimitMax.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, panLimitMin.y, panLimitMax.y);

            lastMousePosition = Input.mousePosition;
        }

        // Smoothly interpolate the camera's position to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * panSmoothing);
    }
} 
