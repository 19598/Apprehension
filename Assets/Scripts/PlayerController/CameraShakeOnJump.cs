using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeOnJump : MonoBehaviour
{
    public IEnumerator ShakeOnJump(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0;
        while (elapsed < duration)
        {
            float x = Random.Range(-0.25f, 0.25f) * magnitude;
            float y = Random.Range(-0.25f, 0.25f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;

    }
}
