using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Actor actor;
    public CollisionBox box;

    float speed = 5f;
    float jump = 50f;
    float gravity = 15f;
    float velocity_x = 0;
    float velocity_y = 0;
    float drag = 0.5f;


    void Start()
    {
       
    }

    void Update()
    {
        GetInput();
        ApplyVelocityAndDrag();
    }
    
    void GetInput()
    {
        velocity_x += Input.GetAxis("Horizontal") * speed;
        if (actor.IsStanding() && Input.GetButtonDown("W")) velocity_y+=jump;
        velocity_y -= gravity;
    }

    void ApplyVelocityAndDrag() {
        actor.MoveX(velocity_x, actor.defaultAction);
        actor.MoveY(velocity_y, actor.defaultAction);
        velocity_x = Mathf.Lerp(velocity_x, 0f, drag);
        velocity_y = Mathf.Lerp(velocity_y, 0f, drag);
    }
}
