using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
#pragma warning disable 649
    [Header("Assignments")]
    [SerializeField] PlayerController PC;
    [SerializeField] MouseLook mouseLook;

    PlayerControl playercontrols;
    PlayerControl.PlayerActions groundMovement;

    Vector2 mouseInput;
    Vector2 horizontalInput;
    float sprintInput;
    float crouchInput;
    //Once the program awakens it calls the methods from the new unity input manager and sets them to the appropriate values/variables.
    private void Awake()
    {
        //Variables.
        playercontrols = new PlayerControl();
        groundMovement = playercontrols.Player;

        //Movement inputs.
        groundMovement.Movement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        groundMovement.Movement.canceled += ctx => horizontalInput = Vector2.zero;

        //Sprint Input.
        groundMovement.Sprint.performed += ctx => sprintInput = ctx.ReadValue<float>();
        groundMovement.Sprint.canceled += ctx => sprintInput = 0;

        //Jump input.
        groundMovement.Jump.performed += _ => PC.OnJumpPressed();

        //Crouch input.
        groundMovement.Crouch.performed += ctx => crouchInput = ctx.ReadValue<float>();
        groundMovement.Crouch.canceled += ctx => crouchInput = 0;

        
        //View inputs
        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        //playercontrols.[action].performed += _ => do something
    }
    private void Update()
    {
        mouseLook.ReceiveInput(mouseInput);
        PC.receiveInput(horizontalInput);
        PC.receiveSprintInput(sprintInput);
        PC.receiveInput(crouchInput);
    }
    private void OnEnable()
    {
        playercontrols.Enable();
    }
    private void OnDestroy()
    {
        playercontrols.Disable();
    } 
}
