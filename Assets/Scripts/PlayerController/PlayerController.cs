using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
#pragma warning disable 649
    //======================================================================================================
    //                                           Assignments
    //                                   All assignment variables
    //======================================================================================================
    [Header("Assignments")]
    [SerializeField] CharacterController controller;//grabs charactercontroller and assigns to controller
    [SerializeField] CameraShakeOnJump cameraShake;//assigns camerashakeonjump to camerashake
    [SerializeField] MouseLook mouseLook;//assigns mouseLook script to use code from that script
    [SerializeField] Health health;//assigns health script to use code from that script
    [SerializeField] Mana mana;//assigns mana script to use code from that script
    [SerializeField] Stamina stamina;//assigns mouseLook script to use code from that script
    [SerializeField] LayerMask groundMask;//recognize ground tag
    //======================================================================================================
    //                                            Variables
    //                          Creates variables for the code to be used with
    //======================================================================================================
    //Stumble
    public float stumbleSpeed = 4;
    public bool stumbling;
    //
    //Vectors
    //
    public Vector2 horizontalInput;//1 or 0
    Vector3 verticalVelocity = Vector3.zero;// velocity of player set to 0 at first
    //
    //Velocities
    //
    public float sprintInput;// 1 or 0
    float continuedMovementX;//deacceleration
    float continuedMovementY;//deacceleration
    float mayJump;//may jump during this time
    float timeInAir;//amount of time in air
    //
    //booleans
    //
    public AudioSource jumpGrunt;
    bool jumpPressed;//Have I pressed jump?
    public bool sprintPressed;//Have I pressed sprint?
    bool sprintButtonPressed;
    public bool hasJumped;//Have I jumped?
    bool landed;//Have I touched ground?
    public bool isGrounded;
    //
    [Header("Player Settings")]
    [SerializeField] public static bool sprintToggle = SharedValues.sharedTogRun;

    public List<GameObject> keys;


    [SerializeField] float walkSpeed = 6f;
    [SerializeField] float jumpHeight = 2.5f;
    [SerializeField] float gravity = -55f;
    [SerializeField] float sprintIncrease = 10f;
    [Header("Current Speed")]
    public float forwardVelocity;
    //======================================================================================================
    //                                           Crouch Variables
    //                                   Creates variables for crouching
    //======================================================================================================
    //
    Vector3 position;//position of character
    RaycastHit hit;//shoots raycast to point
    //
    [HideInInspector] public float crouchInput;//returns 0, or 1 if pressed
    [HideInInspector] public bool checkArea;//Is there a ceiling above me?
    [HideInInspector] public float reducedXClamp = 35f;//changed mouse view to clamp
    private float originalHeight;//original height of character
    [Header("Crouch Settings")]
    public float crouchHeight;//original height/2
    float crouchSpeed = 2f;//crouch speed
    public bool crouched;//Am I crouched?
    //
    public static bool crouchToggle = SharedValues.sharedTogCrouch;//Changes crouching based on toggle
    public bool crouchPressed;//Crouch has been pressed
    bool crouchButtonPressed;
    bool needToCrouch;
    //======================================================================================================
    //                                           Audio Variables
    //                                   Creates variables for audio
    //======================================================================================================
    //Volumes must be betweeen 0 and 1
    //Will find the correct volume for a specific sound by multiplying the master volume by the category of the sound's volume
    //For example, an explosion sound's volume would be found by muliplying masterVolume*sfxVolume
    private float masterVolume = 1f;
    private float musicVolume = 1f;
    private float sfxVolume = 1f;
    private float voiceVolume = 1f;
    //======================================================================================================
    //                                        Receive Input Methods
    //                       Receives input from controls to debate if key was pressed
    //======================================================================================================
    public void receiveInput(Vector2 _horizontalInput)
    { //Receives input from the InputManager.
        horizontalInput = _horizontalInput;
    }

    public void receiveSprintInput(float _sprintInput)
    { //Receives input from the InputManager.
        sprintInput = _sprintInput;
    }
    public void OnJumpPressed()
    { //Sets jump to true if space was pressed.
        jumpPressed = true;
    }
    public void receiveInput(float _crouchInput)
    {
        crouchInput = _crouchInput;
    }
    private void Awake()
    { //On Awake sets all variables(if any) to what they should be.
        forwardVelocity = 0f;
        originalHeight = transform.localScale.y;//org height of char.
        crouchHeight = this.transform.localScale.y / 2;//height / 2
        //QualitySettings.vSyncCount = 0;  // VSync must be disabled
        //Application.targetFrameRate = 60;
    }
    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        HandleJump();
        HandleMovement();

        if (sprintToggle)
        {
            SprintToggle();
        }
        //crouch methods
        CheckPlayerArea();
        HandleCrouch();
        if (crouchToggle)
        {
            CrouchToggle();
        }
        if (stamina.getStamina() <= 5)
        {
            stumbling = true;
        }
        else if (stamina.getStamina() >= stamina.getMaxStamina()/3)
        {
            stumbling = false;
        }
    }
    //==============================================================================================================================
    //                                                          isGrounded
    //                        Checks to see if the player is grounded by using controller.isGrounded
    //==============================================================================================================================
    public void IsGrounded()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            if (landed)
            {
                StartCoroutine(cameraShake.ShakeOnJump(.1f, timeInAir));
                landed = false;
            }
            hasJumped = false;
            mayJump = 0.35f;
            controller.stepOffset = 0.35f;
            verticalVelocity.y = -9.81f;
        }
        else
        {
            landed = true;
            controller.stepOffset = 0f;
            if (mayJump > 0)
            {
                mayJump -= Time.deltaTime;
            }
        }
    }
    //==============================================================================================================================
    //                                                          Jumps
    //                                Checks to see if jump was pressed then activates jumping
    //==============================================================================================================================
    public void HandleJump()
    {
        if (jumpPressed && !crouched && !stumbling)
        {
            if (mayJump > 0f && hasJumped == false)
            {
                jumpGrunt.Play();
                stamina.changeStaminaByAmount(-25f);
                hasJumped = true;
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);

            }
            jumpPressed = false;
        }
        if (crouchPressed || stumbling)
        {
            jumpPressed = false;
        }
        
    }
    //==============================================================================================================================
    //                                                          Movement
    // Handles the movement for the character by using a Vector2 from InputManager and setting each part of the vector to x and y.
    //==============================================================================================================================
    private void HandleMovement()
    {
        if (Mathf.Abs(horizontalInput.x) > 0f || Mathf.Abs(horizontalInput.y) > 0f)
        {
            continuedMovementX = horizontalInput.x;
            continuedMovementY = horizontalInput.y;
            Walk();
            if (controller.isGrounded)
            {
                Sprint();
                Crouch();
                Stumble();
            }
            //applying movement
            Vector3 horizontalVelocity = (transform.right * horizontalInput.x +
            transform.forward * horizontalInput.y) * forwardVelocity;
            controller.Move(horizontalVelocity * Time.deltaTime);

            //gravity
            verticalVelocity.y += gravity * Time.deltaTime;
            controller.Move(verticalVelocity * Time.deltaTime);
        }
        else
        {
            if (forwardVelocity < 0.1f) forwardVelocity = 0;
            //Deceleration
            forwardVelocity = Mathf.Lerp(forwardVelocity, 0, Time.deltaTime * 6);

            //applying movement
            Vector3 horizontalVelocity = (transform.right * continuedMovementX +
            transform.forward * continuedMovementY) * forwardVelocity;
            controller.Move(horizontalVelocity * Time.deltaTime);

            //gravity
            verticalVelocity.y += gravity * Time.deltaTime;
            controller.Move(verticalVelocity * Time.deltaTime);
        }
        
    }
    private void Walk()
    {
        if (crouchInput == 0 && sprintInput == 0 && !checkArea && !crouchPressed && !sprintPressed && !stumbling)
        {
            if (forwardVelocity > walkSpeed - 0.1f) forwardVelocity = walkSpeed;
            //acceleration
            forwardVelocity = Mathf.Lerp(forwardVelocity, walkSpeed, Time.deltaTime * 2);
        }
    }
    private void Sprint()
    {
        if (sprintInput == 1 && !crouched && !stumbling || sprintPressed && !crouched && !stumbling)
        {
            if (forwardVelocity > sprintIncrease - 0.25f) forwardVelocity = sprintIncrease;
            //changes forward velocity with sprinting
            stamina.changeStaminaByAmount(-0.05f);
            forwardVelocity = Mathf.Lerp(forwardVelocity, sprintIncrease, Time.deltaTime);
            if (crouchPressed || crouchInput == 1)
            {
                crouchInput = 0;
                crouchPressed = false;
            }
        }

    }
    private void Crouch()
    {
        if (crouched && sprintInput == 0 && !sprintPressed || checkArea)
        {
            if (forwardVelocity < crouchSpeed + 0.25f) forwardVelocity = crouchSpeed;
            //crouchDeceleration
            forwardVelocity = Mathf.Lerp(forwardVelocity, crouchSpeed, Time.deltaTime * 2);
            if (sprintPressed || sprintInput == 1)
            {
                sprintInput = 0;
                sprintPressed = false;
            }
        }
        if (crouchInput == 1 && sprintInput == 1)
        {
            forwardVelocity = Mathf.Lerp(forwardVelocity, crouchSpeed, Time.deltaTime * 2);
        }
    }
    private void Stumble()
    {
        if (stumbling)
        {
            if (forwardVelocity > stumbleSpeed - 0.1f) forwardVelocity = stumbleSpeed;
            //acceleration
            forwardVelocity = Mathf.Lerp(forwardVelocity, stumbleSpeed, Time.deltaTime * 2);
        }
    }
    private void SprintToggle()
    {
        //Checking Toggles
        if (sprintInput == 1 && !sprintButtonPressed && !crouched)
        { //checks if sprint was pressed and toggles it to the reverse
            sprintButtonPressed = true;
            sprintPressed = !sprintPressed;
        }
        else if (sprintButtonPressed == true && sprintInput == 0) sprintButtonPressed = false;
        //if stopped moving shouldn't still be sprinting
        if (forwardVelocity <= 0) sprintPressed = false;
        if (crouchPressed || crouched || crouchButtonPressed) sprintPressed = false;
    }
    //======================================================================================================
    //                                          Crouch Methods
    //                       All crouch methods beyond this point, for reference.
    //======================================================================================================
    public void CheckPlayerArea()
    {
        checkArea = Physics.BoxCast(position, transform.lossyScale / 2.5f, transform.up, out hit, transform.rotation, originalHeight + 1);
        position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
    }

    private void HandleCrouch()
    {
        if (crouchInput == 1 || crouchPressed) needToCrouch = true;
        else if (crouched && !checkArea) needToCrouch = false;
        //
        if (transform.localScale.y >= crouchHeight && needToCrouch)
        {
            if (transform.localScale.y < crouchHeight + 0.01f) transform.localScale = new Vector3(1, crouchHeight, 1);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, crouchHeight, 1), Time.deltaTime * 12);
        }
        if (transform.localScale.y <= crouchHeight + 0.01f)
        {
            crouched = true;
        }
        //
        if (transform.localScale.y < originalHeight && !needToCrouch)
        {
            if (transform.localScale.y > originalHeight - 0.01f) transform.localScale = new Vector3(1, originalHeight, 1);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, originalHeight, 1), Time.deltaTime * 9);
        }
        if (transform.localScale.y >= originalHeight - 0.01f)
        {
            crouched = false;
        }
        //
        if (checkArea) mouseLook.xClamp = Mathf.Lerp(mouseLook.xClamp, reducedXClamp, Time.deltaTime);
        else mouseLook.xClamp = 85;
    }
    
    private void CrouchToggle()
    {
        if (crouchInput == 1 && !crouchButtonPressed)
        {
            crouchButtonPressed = true;
            crouchPressed = !crouchPressed;
        }
        else if (crouchButtonPressed == true && crouchInput == 0) crouchButtonPressed = false;
        if (sprintPressed) crouchPressed = false;
    }

    void OnDrawGizmosSelected()
    {// Draw a red sphere at the transform's position
        if (crouched && checkArea)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(position + transform.up * hit.distance, transform.lossyScale);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(position, transform.up * originalHeight);
        }
    }
    //======================================================================================================
    //                                          Sound Methods
    //                       Methods for getting and setting the sound settings
    //======================================================================================================
    public float getMasterVolume()
    {
        return masterVolume;
    }
    public void setMasterVolume(float volume)
    {
        masterVolume = volume;
    }
    public float getMusicVolume()
    {
        return musicVolume;
    }
    public void setMusicVolume(float volume)
    {
        musicVolume = volume;
    }
    public float getSfxVolume()
    {
        return sfxVolume;
    }
    public void setSfxVolume(float volume)
    {
        sfxVolume = volume;
    }
    public float getVoiceVolume()
    {
        return voiceVolume;
    }
    public void setVoiceVolume(float volume)
    {
        voiceVolume = volume;
    }
    //======================================================================================================
    //                                          Player Saving and Loading Methods
    //                                           Methods for saving and Loading
    //======================================================================================================
    public void saveGame(string saveName)
    {
        SaveGame.SavePlayer(new PlayerData(this, health, stamina, mana), saveName);
    }

    public void loadGame(string saveName)
    {
        PlayerData player = SaveGame.LoadPlayer(saveName);
        transform.SetPositionAndRotation(new Vector3(player.position[0], player.position[1], player.position[2]), new Quaternion(player.orientation[0], player.orientation[1], player.orientation[2], player.orientation[3]));
        controller.enabled = false;
        controller.transform.SetPositionAndRotation(new Vector3(player.position[0], player.position[1], player.position[2]), new Quaternion(player.orientation[0], player.orientation[1], player.orientation[2], player.orientation[3]));
        controller.enabled = true;
        transform.rotation = new Quaternion(player.orientation[0], player.orientation[1], player.orientation[2], player.orientation[3]);
        health.setHealth(player.health);
        stamina.setStamina(player.stamina);
        mana.setMana(player.mana);
    }
}
