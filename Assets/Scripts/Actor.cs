using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Actor : Actorsolid
{
    Vector2 velocity;
    float drag = 0.01f;

    public delegate void CollisionAction();
    CollisionAction defaultAction;
    CollisionAction squishAction;
    
    void Start()
    {
        squishAction = Squish;
        
        StartPosition();
        box = GetComponent<Collider2D>();
        Actortracker.actorList.Add(this);
    }

    void FixedUpdate()
    {
        FixPosition();
    }

    bool CollideCheck(int x_check, int y_check, Solid solid)
    {
        Bounds place = new Bounds(new Vector3(x_check, y_check), box.bounds.size);
        return place.Intersects(solid.box.bounds);
    }
    bool CollideCheck(int x_check, int y_check)
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    void Squish()
    {

    }
    public void MoveX(float distance, CollisionAction action)
    {
        float remainder = distance;
        int move = Mathf.RoundToInt(distance);
        int direction = Math.Sign(move);

        while (move != 0)
        {
            if(CollideCheck(x+direction, y))
        }
    }
    public void MoveY(float distance, CollisionAction action)
    {
        float remainder = distance;
        int move = Mathf.RoundToInt(distance);
        int direciton = Math.Sign(move);


    }
}
