using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public Actor actor;
    public CollisionBox box;

    float speed = 3f;
    float jump = 3f;
    float jumpHold = 0.2f;
    int jumpHoldTime = 6;
    float jumpPeakGravityReducer = 0.7f;
    float jumpGravityThreshold = 0.1f;
    int hangTime = 20;
    float gravity = 0.2f;
    float max_velocity_x = 5f;
    float stoppingBonus = 0.7f;
    float velocity_x = 0;
    float velocity_y = 0;
    float drag = 0.8f;

    private Actor.CollisionAction resetX;
    private Actor.CollisionAction resetY;

    public StateMach stateMachine;

    private InputBuffering inputBuffer;

    void Start()
    {
        stateMachine = new StateMach(State_Run, State_Jump);
        resetX = () => { velocity_x = 0; };
        resetY = () => { velocity_y = 0; };

        inputBuffer = GetComponent<InputBuffering>();
        inputBuffer.AddAxis("Vertical", .8f);
    }

    void FixedUpdate()
    {
        stateMachine.Execute();
    }

    void ApplyVelocityAndDrag() {
        actor.MoveX(velocity_x, resetX);
        actor.MoveY(velocity_y, resetY);
        velocity_x = Mathf.Lerp(velocity_x, 0f, drag);
    }

    #region //states
    
    void State_Run()
    {
        float horizontal = Input.GetAxis("Horizontal");
        InputBuffer verticalInput = inputBuffer.GetInputBuffer("Vertical");
        bool vertical = verticalInput.state==BufferStates.Down || (verticalInput.time < verticalInput.decay);
        if (horizontal == 0f) velocity_x *= stoppingBonus;
        velocity_x = Mathf.Clamp(-1 * max_velocity_x, velocity_x + horizontal * speed, max_velocity_x);
        if (actor.IsStanding() && vertical) stateMachine.ChangeState(1, 0);

        velocity_y -= gravity;

        ApplyVelocityAndDrag();
    }
    void State_Jump()
    {
        float horizontal = Input.GetAxis("Horizontal");
        velocity_x = Mathf.Clamp(-1 * max_velocity_x, velocity_x + horizontal, max_velocity_x);
        
        switch (stateMachine.subState)
        {
            case 0:
                if (inputBuffer.GetInputBuffer("Vertical").state == BufferStates.Held && stateMachine.stateTimer < jumpHoldTime)
                {
                    Debug.Log(stateMachine.stateTimer);
                    if (stateMachine.stateTimer == 0) velocity_y += jump;
                    else velocity_y += jumpHold;
                }
                else
                {
                    stateMachine.ChangeState(1, 1);
                    velocity_y -= gravity;
                }
                break;

            case 1:
                if (!(velocity_y > jumpGravityThreshold)) velocity_y -= jumpPeakGravityReducer * gravity;
                else velocity_y -= gravity;
                if (velocity_y < jumpGravityThreshold) stateMachine.ChangeState(1, 2);
                break;

            case 2:
                velocity_y -= gravity;
                break;
        }
        ApplyVelocityAndDrag();
        if (actor.IsStanding()) stateMachine.ChangeState(0);
    }
    void State_Pound()
    {

    }
    void State_Die()
    {

    }
    #endregion
}
