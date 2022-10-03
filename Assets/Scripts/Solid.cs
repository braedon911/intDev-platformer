using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class Solid : Actorsolid
{
    private bool collidable = false;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move(float x_distance, float y_distance)
    {

        
    }
}
