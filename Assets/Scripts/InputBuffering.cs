using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBuffering : MonoBehaviour
{
    List<InputBuffer> bufferMap;
    List<string> verifyList;

    void AddAxis(string axisName, float decay = 0f)
    {
        if(!verifyList.Contains(axisName)) bufferMap.Add(new InputBuffer(axisName, decay));
    }
    public InputBuffer GetInputBuffer(string axisName)
    {
        InputBuffer buffer = bufferMap[verifyList.FindIndex((string s) => {return s == axisName; })];

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
                axis.state = axis.state == BufferStates.NotPressed ? BufferStates.Down : BufferStates.Held;
            }
            else
            {
                if (axis.releasedTime < axis.decay)
                {
                    if (axis.releasedTime == 0f) axis.state = BufferStates.Up;
                    else axis.state = BufferStates.Decay;

                    axis.releasedTime += Time.deltaTime;
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
    public InputBuffer(InputBuffer buffer)
    {
        //WIP copy constructor
        buffer.axisName;
    }

    public void Reset()
    {
        time = 0f;
        releasedTime = 0f;
        state = BufferStates.NotPressed;
    }

    public float decay;
    public string axisName;
    public float time = 0f;
    public float releasedTime = 0f;
    public BufferStates state = BufferStates.NotPressed;


}
public enum BufferStates
{
    NotPressed,
    Decay,
    Down,
    Up,
    Held
}