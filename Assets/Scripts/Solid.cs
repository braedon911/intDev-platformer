using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CollisionBox)), System.Serializable]
public class Solid : Actorsolid
{
    private bool collidable = true;
    public override bool IsSolid() { return true; }

    void Start()
    {
        box = GetComponent<CollisionBox>();
    }
    public void Move(float x_distance, float y_distance)
    {

    }
}
