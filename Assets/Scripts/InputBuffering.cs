using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBuffering : MonoBehaviour
{
    List<InputBuffer> bufferMap;

    void AddAxis(string axisName, float decay = 0f)
    {
        bufferMap.Add(new InputBuffer(axisName, decay));   
    }
    public InputBuffer GetInputBuffer(string axisName)
    {

    }
    void Start()
    {
        
    }

    void Update()
    {
        foreach (var axis in bufferMap)
        {
            if (Input.GetAxis(axis.axisName) != 0)
            {
                axis.time += Time.deltaTime;
                axis.state = axis.state == "NotPressed" ? "Down" : "Held";
            }
            else
            {
                axis.releasedTime += Time.deltaTime;

                if (axis.time + axis.releasedTime < axis.decay)
                {
                    axis.state = "Decay";
                }
                else
                {
                    axis.Reset();
                }
            }
            
        }
    }
}
public class InputBuffer
{
    public InputBuffer(string axisName, float decay)
    {
        this.axisName = axisName;
        this.decay = decay;
    }

    public void Reset()
    {
        time = 0f;
        releasedTime = 0f;
        state = "NotPressed";
    }

    public float decay;
    public string axisName;
    public float time = 0f;
    public float releasedTime = 0f;
    public string state = "NotPressed";


}
