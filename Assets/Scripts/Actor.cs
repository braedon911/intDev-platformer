using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CollisionBox))]
public class Actor : Actorsolid
{
    public delegate void CollisionAction();
    public CollisionAction defaultAction = () => { };
    public CollisionAction squishAction;

    public override bool IsSolid() { return false; }

    void Start()
    {
        squishAction = Squish;
        box = gameObject.GetComponent<CollisionBox>();
        Actortracker.actorList.Add(this);
    }

    public bool CollideCheck(int x_check, int y_check)
    {
        Func<CollisionBox, bool> qualifier = (box) => 
        {
            Solid solid = box.GetComponent<Solid>();
            bool isSolid = (solid != null) && (solid.collidable==true);
            return isSolid;
        };
        return box.PlaceMeeting(x_check, y_check, qualifier);
    }

    public bool IsStanding()
    {
        return CollideCheck(X, Y - 1);
    }

    void Squish()
    {
        //die or something
    }

    public bool IsRiding(Solid solid)
    {
        bool hitSolid = false;
        hitSolid = box.InstancePlace(X, Y - 1)?.GetComponent<Solid>() == solid;
        return hitSolid;
    }

    float xRemainder = 0f;
    float yRemainder = 0f;
    public void MoveX(float distance, CollisionAction action)
    {
        xRemainder += distance;
        int move = Mathf.RoundToInt(xRemainder);

        if (move != 0)
        {
            xRemainder -= move;
            int direction = Math.Sign(move);

            while (move != 0)
            {

                if (!CollideCheck(X + direction, Y))
                {
                    X += direction;
                    move -= direction;
                }
                else
                {
                    action();
                    break;
                }
            }
        }
    }
    public void MoveY(float distance, CollisionAction action)
    {
        yRemainder += distance;
        int move = Mathf.RoundToInt(yRemainder);

        if (move != 0)
        {
            yRemainder -= move;
            int direction = Math.Sign(move);

            while (move != 0)
            {
                if (!CollideCheck(X, Y + direction))
                {
                    Y += direction;
                    move -= direction;
                }
                else
                {
                    action();
                    break;
                }
            }
        }
    }
}

