using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBuffering : MonoBehaviour
{
    List<InputBuffer> bufferMap;
    List<string> verifyList;

    public void AddAxis(string axisName, float decay = 0f)
    {
        if (verifyList!=null && !verifyList.Contains(axisName))
        {
            verifyList.Add(axisName);
            bufferMap.Add(new InputBuffer(axisName, decay));
        }
    }
    public InputBuffer GetInputBuffer(string axisName)
    {
        InputBuffer buffer = bufferMap[verifyList.FindIndex((string s) => {return s == axisName; })];
        return buffer;
    }
    void Awake()
    {
        bufferMap = new List<InputBuffer>();
        verifyList = new List<string>();
    }
    void Update()
    {
        foreach (var axis in bufferMap)
        {
            axis.value = Input.GetAxisRaw(axis.axisName);
            if (Input.GetAxisRaw(axis.axisName) != 0)
            {
                if (axis.releasedTime > 0f)
                {
                    axis.Reset();
                }
                axis.state = axis.time == 0f? BufferStates.Down : BufferStates.Held;
                axis.time += Time.deltaTime;
            }
            else
            {
                if (axis.releasedTime < axis.decay && axis.time != 0f)
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
        buffer.axisName = axisName;
        buffer.decay = decay;
        buffer.time = time;
        buffer.releasedTime = releasedTime;
        buffer.state = state;
        buffer.value = value;
    }

    public void Reset()
    {
        time = 0f;
        releasedTime = 0f;
        state = BufferStates.NotPressed;
        value = 0f;
    }

    public float value = 0f;
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