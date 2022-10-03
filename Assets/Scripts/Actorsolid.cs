using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Actorsolid : MonoBehaviour
{
    protected int x = 0;
    protected int y = 0;
    protected Vector2 origin = new Vector2Int();
    public Collider2D box;

    protected void StartPosition()
    {
        x = Mathf.RoundToInt(transform.position.x);
        y = Mathf.RoundToInt(transform.position.y);
        origin = new Vector2Int(x, y);
    }
    protected Vector2 FixPosition()
    {
        Vector2 fixedVec = new Vector2(x, y);
        transform.position = fixedVec;
        return fixedVec;
    }
}
public static class Actortracker
{
    Physics.autoSimulation = false;
    public static List<Actor> actorList = new List<Actor>();
}