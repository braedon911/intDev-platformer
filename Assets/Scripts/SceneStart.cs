using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStart : MonoBehaviour
{
    private void Update()
    {
        if (Time.time > 10f) SceneManager.LoadScene("SampleScene");
    }
}
