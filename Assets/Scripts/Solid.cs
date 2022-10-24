using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CollisionBox)), System.Serializable]
public class Solid : Actorsolid
{
    public bool collidable = true;
    public override bool IsSolid() { return true; }

    void Start()
    {
        box = GetComponent<CollisionBox>();
    }

    float xRemainder = 0f;
    float yRemainder = 0f;
    public void Move(float x_distance, float y_distance)
    {
        xRemainder += x_distance;
        yRemainder += y_distance;

        int move_x = Mathf.RoundToInt(xRemainder);
        int move_y = Mathf.RoundToInt(yRemainder);

        if (move_x != 0 || move_y != 0)
        {
            List<Actor> riderList = new List<Actor>();
            foreach(Actor actor in Actortracker.actorList)
            {
                if (actor.IsRiding(this)) riderList.Add(actor);
            }

            //collidable = false;
        }
    }
}
