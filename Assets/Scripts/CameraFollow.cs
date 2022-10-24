using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    float pivotX;
    float pivotY;
    float smoothing = 0.9f;

    float shakeIntensity = 25f;
    float screenshakeleft = 0f;

    void Update()
    {
        pivotX = target.transform.position.x;
        pivotY = target.transform.position.y + 64;

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, pivotX, smoothing), Mathf.Lerp(transform.position.y, pivotY, smoothing), transform.position.z);
    }
    public void Screenshake(float duration)
    {
        screenshakeleft = duration;
        StartCoroutine(DoScreenshake());
    }
    IEnumerator DoScreenshake()
    {
        if (screenshakeleft > 0)
        {
            Debug.Log("s");
            transform.position = new Vector3(transform.position.x + Random.Range(-1f, 1f) * shakeIntensity, transform.position.y + Random.Range(-1f, 1f) * shakeIntensity, transform.position.z);
        }
        else StopCoroutine(DoScreenshake());

        screenshakeleft -= Time.deltaTime/60f;
        yield return null;
    }
}
