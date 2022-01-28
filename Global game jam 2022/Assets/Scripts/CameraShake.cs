using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = transform.position.x + Random.Range(-1f, 1f) * magnitude;
            float y = transform.position.y + Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(x, y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
