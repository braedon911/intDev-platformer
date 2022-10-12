using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMach
{
    public List<StateFunction> states;
    public int stateTimer = 0;
    public int subState = 0;
    public StateFunction state;

    public delegate void StateFunction();

    public StateMach(params StateFunction[] _states)
    {
        states = new List<StateFunction>();
        foreach (StateFunction state in _states)
        {
            states.Add(state);
        }
        ChangeState(0);
    }
    public void ChangeState(int index, int new_substate = 0)
    {
        state = states[index];
        subState = new_substate;
    }
    public void Execute()
    {
        state();
        stateTimer += 1;
    }
}
