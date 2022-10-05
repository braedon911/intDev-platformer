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

    bool CollideCheck(int x_check, int y_check)
    {
        Func<CollisionBox, bool> qualifier = (box) => { return box.gameObject.GetComponent<Solid>() != null; };
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
    public void MoveX(float distance, CollisionAction action)
    {
        float remainder = distance;
        int move = Mathf.RoundToInt(distance);
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
    public void MoveY(float distance, CollisionAction action)
    {
        float remainder = distance;
        int move = Mathf.RoundToInt(distance);
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

