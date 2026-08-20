using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    public float CameraMoveSpeed = 0.1F;
    Vector2 mousePosition;
    Vector3 mousePositionVec3;
    InputAction mouseMove;
    InputAction mouseClick;
    InputAction cameraZoom;
    public Camera mainCamera;
    public float zoomSpeed = 0.2f;
    public float minZoom, maxZoom;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouseMove = InputSystem.actions["MouseMove"];
        mouseClick = InputSystem.actions["MouseClick"];
        cameraZoom = InputSystem.actions["Zoom"];
        mainCamera = GetComponentInChildren<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        mousePosition= mouseMove.ReadValue<Vector2>();
        mousePositionVec3 = new Vector3(mousePosition.x,0, mousePosition.y);
        mainCamera.orthographicSize = Mathf.Clamp((mainCamera.orthographicSize + cameraZoom.ReadValue<float>() * zoomSpeed), minZoom, maxZoom);
        if (mouseClick.IsPressed())
        {
            this.transform.Translate(mousePositionVec3*CameraMoveSpeed,Space.Self);
        }  
        
    }
}
