using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public Actor actor;
    public CollisionBox box;

    int startX = 0;
    int startY = 0;

    float jump = 3f;
    float jumpHold = 0.2f;
    int jumpHoldTime = 6;
    float jumpPeakGravityReducer = 0.7f;
    float jumpGravityThreshold = 0.1f;
    int coyoteFrames = 3;

    int hangTime = 15;
    float prePoundSlow = 0.6f;
    float poundImpulse = 5f;

    float gravity = 0.2f;
    float max_velocity_x = 6f;
    float stoppingBonus = 0.7f;
    float speed = 3f;

    public float velocity_x = 0;
    public float velocity_y = 0;
    float drag = 0.8f;

    private Actor.CollisionAction resetX;
    private Actor.CollisionAction resetY;

    public StateMach stateMachine;

    private InputBuffering inputBuffer;

    public AudioSource jumpSound;
    public AudioSource landSound;

    void Start()
    {
        stateMachine = new StateMach(State_Run, State_Jump, State_Pound, State_Die);
        resetX = () => { velocity_x = 0; };
        resetY = () => { velocity_y = 0; };

        inputBuffer = GetComponent<InputBuffering>();
        inputBuffer.AddAxis("Vertical", .8f);

        actor.squishAction = () =>
        {
            stateMachine.ChangeState(3);
        };
    }

    void FixedUpdate()
    {
        stateMachine.Execute();
        if (actor.box.PlaceMeeting(actor.X, actor.Y, (box) => { return box.GetComponent<KillZone>(); })) stateMachine.ChangeState(3);
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
        bool vertical = verticalInput.value > 0f && (verticalInput.state==BufferStates.Down || (verticalInput.time!=0 && verticalInput.time < verticalInput.decay));

        if (horizontal == 0f) velocity_x *= stoppingBonus;
        velocity_x = Mathf.Clamp(-1 * max_velocity_x, velocity_x + horizontal * speed, max_velocity_x);

        if (actor.IsStanding() && vertical)
        {
            jumpSound.Play();
            stateMachine.ChangeState(1, 0);
        }
        else if (!actor.IsStanding())
        {
            stateMachine.ChangeState(1, 3);
        }
        
        

        ApplyVelocityAndDrag();
    }
    void State_Jump()
    {
        float horizontal = Input.GetAxis("Horizontal");
        velocity_x = Mathf.Clamp(-1 * max_velocity_x, velocity_x + horizontal * speed, max_velocity_x);
        InputBuffer verticalInput = inputBuffer.GetInputBuffer("Vertical");

        if(verticalInput.value < 0f)
        {
            bool pound = verticalInput.state == BufferStates.Down || (verticalInput.time != 0 && verticalInput.time < verticalInput.decay);
            if (pound)
            {
                stateMachine.ChangeState(2, 0);
            }
        }
        else
        {
            switch (stateMachine.subState)
            {
                case 0:
                    if (inputBuffer.GetInputBuffer("Vertical").state == BufferStates.Held && stateMachine.stateTimer < jumpHoldTime)
                    {
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
                case 3:
                    bool vertical = verticalInput.value > 0f && (verticalInput.state == BufferStates.Down || (verticalInput.time != 0 && verticalInput.time < verticalInput.decay));
                    if (stateMachine.stateTimer < coyoteFrames && vertical) stateMachine.ChangeState(1,0);
                    if (!(velocity_y > jumpGravityThreshold)) velocity_y -= jumpPeakGravityReducer * gravity;
                    else velocity_y -= gravity;
                    if (velocity_y < jumpGravityThreshold) stateMachine.ChangeState(1, 2);
                    break;
            }
        }

        ApplyVelocityAndDrag();
        if (actor.IsStanding()) stateMachine.ChangeState(0);
    }
    void State_Pound()
    {
        switch (stateMachine.subState)
        {
            case 0:
                velocity_y *= prePoundSlow;
                if (stateMachine.stateTimer > hangTime)
                {
                    velocity_y -= poundImpulse;
                    velocity_x = 0;
                    stateMachine.ChangeState(2, 1);
                }
                break;
            case 1:
                velocity_y -= gravity;
                Breakable hit;
                hit = (Breakable)(box.InstancePlace(actor.X, actor.Y + Mathf.RoundToInt(velocity_y))?.GetComponent("Breakable"));
                if (hit!=null)
                {
                    landSound.Play();
                    hit.Break();
                }
                else if(actor.CollideCheck(actor.X, actor.Y + Mathf.RoundToInt(velocity_y)))
                {
                    stateMachine.ChangeState(2, 2);
                    landSound.Play();
                    Camera.main.GetComponent<CameraFollow>().Screenshake(0.4f);
                }
                break;
            case 2:
                velocity_y = 0;
                //do ground pound
                if (stateMachine.stateTimer > 6) stateMachine.ChangeState(0);
                break;
        }
        ApplyVelocityAndDrag();
    }
    void State_Die()
    {
        actor.box.lockedPosition.x = startX;
        actor.box.lockedPosition.y = startY;

        velocity_x = 0f;
        velocity_y = 0f;

        if (stateMachine.stateTimer > 30) stateMachine.ChangeState(0);
    }
    #endregion
}
