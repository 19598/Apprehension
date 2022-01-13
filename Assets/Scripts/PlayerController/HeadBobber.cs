using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobber : MonoBehaviour
{
    public AudioSource leftFoot;
    public AudioSource rightFoot;
    public Camera mainCamera;
    public Vector3 mainCameraOrigin;
    public Vector3 targetBobPosition;

    private void Start()
    {
        mainCameraOrigin = mainCamera.transform.localPosition;
    }
    public void HeadBob(float z, float yIntensity)
    {
        targetBobPosition = mainCameraOrigin + new Vector3(0, Mathf.Sin(z * 4) * yIntensity, 0);

        if (targetBobPosition.y <= 0.002f || targetBobPosition.y >= 0.002f)
        {
            random();
        }
    }
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
