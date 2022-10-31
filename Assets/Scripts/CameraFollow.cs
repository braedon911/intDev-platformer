using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    float pivotX;
    float pivotY;
    float smoothing = 0.9f;

    float shakeIntensity = 5f;
    float screenshakeleft = 0f;

    public int offset = 0;
    void Update()
    {
        pivotX = target.transform.position.x;
        pivotY = target.transform.position.y - offset;

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, pivotX, smoothing), Mathf.Lerp(transform.position.y, pivotY, smoothing), transform.position.z);
    }
    public void Screenshake(float duration)
    {
        screenshakeleft = duration;
        StartCoroutine(DoScreenshake());
    }
    IEnumerator DoScreenshake()
    {
        float shakeToStart = screenshakeleft;
        while(screenshakeleft > 0)
        {
            float ratio = screenshakeleft / shakeToStart;
            transform.position = new Vector3(transform.position.x + Random.Range(-1f, 1f) * shakeIntensity * ratio, transform.position.y + Random.Range(-1f, 1f) * shakeIntensity * ratio, transform.position.z);
            screenshakeleft -= Time.deltaTime;
            yield return null;
        }
        StopCoroutine(DoScreenshake());

        
    }
}
