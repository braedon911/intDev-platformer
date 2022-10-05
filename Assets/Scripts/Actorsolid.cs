using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Actortracker
{
    public static List<Actor> actorList = new List<Actor>();
}
public class Actorsolid : MonoBehaviour
{
    public CollisionBox box;
    public int X { get { return box.lockedPosition.x; } set { box.lockedPosition.x = value; } }
    public int Y { get { return box.lockedPosition.y; } set { box.lockedPosition.y = value; } }

    public virtual bool IsSolid() { return false; }
}
static class BoxSystem
{
    public static List<CollisionBox> boxList = new List<CollisionBox>();
}
