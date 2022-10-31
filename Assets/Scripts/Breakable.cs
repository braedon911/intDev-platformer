using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public void Break()
    {
        if(TryGetComponent(out Solid solid)) solid.collidable = false;
        Destroy(gameObject);
    }
}
