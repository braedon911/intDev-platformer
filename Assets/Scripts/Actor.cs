using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Actor : Actorsolid
{
    Vector2 velocity;
    float drag = 0.01f;

    public delegate void defaultCollisionAction();
    public delegate void squishCollissionAction();

    
    void Start()
    {
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    void Squish()
    {

    }
    public void MoveX(float distance, Action callback)
    {

    }
    public void MoveY()
    {

    }
}
