using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadBob : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] HeadBobber headBobber;
    float idleCounter;
    float movementCounter;
    float sprintCounter;
    float crouchCounter;
    float stumbleCounter;
    public float sprintMultiplier = 0;
    public float multiplier = 3f;
    float multiplierVelocity;
    public static bool headBobbing = SharedValues.headBob;
    private void Update()
    {
        if (headBobbing)
        {
            HandleBob();
        }
    }
    //Handles the bobbing mechanics of the camera and uses intensity and sends the intensity to the headbobber method to act like breathing.
    private void HandleBob()
    {
        if (Mathf.Abs(playerController.horizontalInput.x) > 0f || Mathf.Abs(playerController.horizontalInput.y) > 0f)
        {
            if (!playerController.stumbling)
            {
                WalkBob();
                if (playerController.sprintInput > 0 || playerController.sprintPressed)
                {
                    SprintBob();
                }
                if (playerController.crouchInput > 0 || playerController.crouchPressed)
                {
                    CrouchBob();
                }
            }
            else
            {
                StumbleBob();

            }
        }
        else
        {
            IdleBob();
        }
        Count();
    }
    private void WalkBob()
    {
        multiplierVelocity = Mathf.Lerp(multiplier, 5, Time.deltaTime);

        headBobber.HeadBob(movementCounter, .1f);
        headBobber.mainCamera.transform.localPosition = Vector3.Lerp(
               headBobber.mainCamera.transform.localPosition,
               headBobber.targetBobPosition, Time.deltaTime * 8);
    }
    private void SprintBob()
    {
        headBobber.HeadBob(sprintCounter, .25f);
        headBobber.mainCamera.transform.localPosition = Vector3.Lerp(
         headBobber.mainCamera.transform.localPosition,
         headBobber.targetBobPosition, Time.deltaTime * 8);
    }
    private void CrouchBob()
    {
        headBobber.HeadBob(crouchCounter, .1f);
        headBobber.mainCamera.transform.localPosition = Vector3.Lerp(
               headBobber.mainCamera.transform.localPosition,
               headBobber.targetBobPosition, Time.deltaTime * 5);
    }
    private void IdleBob()
    {
        multiplierVelocity = Mathf.Lerp(multiplier, 0, Time.deltaTime);

        headBobber.HeadBob(idleCounter, .05f);
        headBobber.mainCamera.transform.localPosition = Vector3.Lerp(
               headBobber.mainCamera.transform.localPosition,
               headBobber.targetBobPosition, Time.deltaTime * 2);
    }
    private void StumbleBob()
    {
        headBobber.HeadBob(stumbleCounter, .25f);
        headBobber.mainCamera.transform.localPosition = Vector3.Lerp(
               headBobber.mainCamera.transform.localPosition,
               headBobber.targetBobPosition, Time.deltaTime * 5);
    }

    private void Count()
    {
        movementCounter += Time.deltaTime * multiplierVelocity;
        idleCounter += Time.deltaTime;
        sprintCounter += Time.deltaTime * 5;
        crouchCounter += Time.deltaTime * multiplierVelocity;
        stumbleCounter += Time.deltaTime;
    }
}
