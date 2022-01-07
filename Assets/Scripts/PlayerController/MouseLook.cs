using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
#pragma warning disable 649
    float mouseX, mouseY;
    float xRotation = 0f;
    float posX = 0f;
    float posY = 0f;

    [Header("Assignments")]
    [SerializeField] Transform playerCamera;
    public float speed = 4;

    [Header("Player Settings")]
    [SerializeField] float sensitivityX = 30f;
    [SerializeField] float sensitivity = 30f;
    public float xClamp = 120f;
    //Locks the mouse cursor to the screen
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        HandleRotation();
    }
    //handles the rotation of the screen by rotating the y axis to move the camera sideways
    //and rotating the cameras x axis to rotate the camera vertically.
    Vector3 leanedPos;
    private void HandleRotation()
    {

        transform.Rotate(Vector3.up, mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        playerCamera.eulerAngles = targetRotation;

        leanedPos = new Vector3(playerCamera.localPosition.x - posX, playerCamera.localPosition.y - posY, playerCamera.localPosition.z);
        playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, leanedPos, speed);
    }

    //Receives input from the InputManager class.
    public void ReceiveInput(Vector2 mouseInput)
    {
        mouseX = mouseInput.x * sensitivityX * SharedValues.sharedSens;
        mouseY = mouseInput.y * sensitivity * SharedValues.sharedSens;
    }
    public void setPosX(float value)
    {
        posX = value;
    }
    public void setPosY(float value)
    {
        posY = value;
    }
    
}
