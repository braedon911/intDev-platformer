using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    void Update()
    {
        transform.localScale = new Vector3(Mathf.Cos(Time.time), 1, 1);
    }
}
