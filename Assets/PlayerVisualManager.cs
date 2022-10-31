using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualManager : MonoBehaviour
{
    public SpriteRenderer[] renderers;

    int facing = 1;
    public bool doFacing = true;
    float flippedOffset;
    void Start()
    {
        if (doFacing)
        {
            flippedOffset = renderers[0].localBounds.size.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (doFacing)
        {
            float axis = Mathf.Sign(Input.GetAxis("Horizontal"));

            //facing = 
        }
        
    }
}
