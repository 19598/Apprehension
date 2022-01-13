using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeOnJump : MonoBehaviour
{

    /// <summary>
    /// Makes the camera shake when the player jumps
    /// </summary>
    /// <param name="duration">How long the camera shakes</param>
    /// <param name="magnitude">Intensity of the shake</param>
    /// <returns></returns>
    public IEnumerator ShakeOnJump(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0;
        //while the user is jumping,
        while (elapsed < duration)
        {
            float x = Random.Range(-0.25f, 0.25f) * magnitude;
            float y = Random.Range(-0.25f, 0.25f) * magnitude;

            //shake
            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;

    }
}
