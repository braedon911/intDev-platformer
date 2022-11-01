using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform[] waypoints;
    int target = 0;
    public float speed;
    int waitFrames = 60;
    int counter = 30;

    Solid solid;
    void Start()
    {
        solid = GetComponent<Solid>();
    }

    void Update()
    {
        var targetPos = waypoints[target].position;
        
        if (solid.box.PointInBox((int)targetPos.x, (int)targetPos.y))
        {
            counter--;
            if(counter < 1)
            {
                counter = waitFrames;
                NextTarget();
            }
        }
        else
        {
            float distance_x, distance_y;
            float gap_x = targetPos.x - solid.X;
            float gap_y = targetPos.y - solid.Y;
            distance_x = Mathf.Clamp(speed * Mathf.Sign(gap_x), -1 * Mathf.Abs(gap_x), Mathf.Abs(gap_x));
            distance_y = Mathf.Clamp(speed * Mathf.Sign(gap_y), -1 * Mathf.Abs(gap_y), Mathf.Abs(gap_y));

            solid.Move(distance_x, distance_y);
        }
    }
    void NextTarget()
    {
        int range = waypoints.Length - 1;
        int wrap = target == range ? 0 : target + 1;

        target = wrap;
    }
}
