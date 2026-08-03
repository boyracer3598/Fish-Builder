using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    public float CameraMoveSpeed = 0.1F;
    Vector2 mousePosition;
    Vector3 mousePositionVec3;
    InputAction mouseMove;
    InputAction mouseClick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouseMove= InputSystem.actions["MouseMove"];
        mouseClick= InputSystem.actions["MouseClick"];
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition= mouseMove.ReadValue<Vector2>();
        mousePositionVec3 = new Vector3(mousePosition.x,0, mousePosition.y);
        if (mouseClick.IsPressed())
        {
            Debug.Log("mouse is pressed");
            Debug.Log(mousePosition.ToString());
            this.transform.Translate(mousePositionVec3*CameraMoveSpeed,Space.Self);
        }  
        
    }
}
