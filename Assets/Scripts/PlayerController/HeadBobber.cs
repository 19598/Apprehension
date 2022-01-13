using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobber : MonoBehaviour
{
    //audio sources for left and right
    public AudioSource leftFoot;
    public AudioSource rightFoot;
    //main camera assignment
    public Camera mainCamera;
    //the original point of the camera
    public Vector3 mainCameraOrigin;
    //the position that the camera will want to go to
    public Vector3 targetBobPosition;

    private void Start()
    {
        //assigns the original point of the camera
        mainCameraOrigin = mainCamera.transform.localPosition;
    }
    //uses math.sin to demostrate head bobbing in real life when you take a breath in and out and scales it depending on if the player is running, walking, stumbling, idling etc.
    public void HeadBob(float z, float yIntensity)
    {
        targetBobPosition = mainCameraOrigin + new Vector3(0, Mathf.Sin(z * 4) * yIntensity, 0);

        if (targetBobPosition.y <= 0.002f || targetBobPosition.y >= 0.002f)
        {
            random();
        }

    }
    //randomly picks a number between 1 and 2 and whichever number is selected a audio sound will play 
    private void random()
    {
        int num = Random.Range(1, 2);

        if (num == 1)
        {
            leftFoot.Play();
        }
        else
        {
            rightFoot.Play();
        }
    }
}
