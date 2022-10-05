using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Actor actor;
    public CollisionBox box;

    float speed = .5f;
    float jump = 2f;
    float gravity = 0.05f;
    float max_velocity_x = 1f;
    float velocity_x = 0;
    float velocity_y = 0;
    float drag = 0.8f;

    private Actor.CollisionAction resetX;
    private Actor.CollisionAction resetY;

    void Start()
    {
        resetX = () => { velocity_x = 0; };
        resetY = () => { velocity_y = 0; };
    }

    void Update()
    {
        GetInput();
        ApplyVelocityAndDrag();
    }
    
    void GetInput()
    {
        velocity_x = Mathf.Clamp(-1*max_velocity_x, velocity_x + (Input.GetAxis("Horizontal") * speed), max_velocity_x);
        if (actor.IsStanding() && Input.GetKeyDown(KeyCode.Space)) velocity_y+=jump;
        
    }

    void ApplyVelocityAndDrag() {
        actor.MoveX(velocity_x, resetX);
        actor.MoveY(velocity_y, resetY);
        velocity_x = Mathf.Lerp(velocity_x, 0f, drag);
        if (Mathf.Round(velocity_y) != 0f) velocity_y -= gravity; else velocity_y -= gravity / 10f;
    }
}
