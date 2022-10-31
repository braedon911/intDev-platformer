using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionBox : MonoBehaviour
{
    [SerializeField]
    private Vector2Int dimensions;
    public Vector2Int lockedPosition;
    public Vector3Int lockedScale;
    private Vector2Int topLeft, bottomRight;

    public Vector2Int Dimensions { get { return dimensions; } set { } }
    public Vector2Int TopLeft { get { return topLeft + lockedPosition; } set { } }
    public Vector2Int TopRight { get { return dimensions + lockedPosition; } set { } }
    public Vector2Int BottomLeft { get { return lockedPosition; } set { } }
    public Vector2Int BottomRight { get { return bottomRight + lockedPosition; } set { } }

    public SpriteRenderer spriteRenderer;
    public bool PointInBox(int x_check, int y_check)
    {
        return ((x_check <= TopRight.x && x_check >= BottomLeft.x) && (y_check <= TopRight.y && y_check >= BottomLeft.y));
    }
    public bool PointInBox(Vector2Int checkPosition)
    {
        int x_check = checkPosition.x;
        int y_check = checkPosition.y;
        return PointInBox(x_check, y_check);
    }
    public bool PlaceMeeting(int x_check, int y_check, Func<CollisionBox, bool> qualifier)
    {
        Vector2Int checkPosition = new Vector2Int(x_check, y_check);
        bool check = false;
        foreach (CollisionBox box in BoxSystem.boxList)
        {
            if (box == null) {
                BoxSystem.boxList.Remove(box);
                break;
            }
            if (box != this) {
                check = box.PointInBox(checkPosition) || box.PointInBox(dimensions + checkPosition) || box.PointInBox(new Vector2Int(dimensions.x, 0) + checkPosition) || box.PointInBox(new Vector2Int(0, dimensions.y) + checkPosition);

                if (check && qualifier(box)) break;
            }            
        }
        return check;
    }
    public bool PlaceMeeting(int x_check, int y_check)
    {
        Vector2Int checkPosition = new Vector2Int(x_check, y_check);
        bool check = false;
        foreach (CollisionBox box in BoxSystem.boxList)
        {
            if (box == null)
            {
                BoxSystem.boxList.Remove(box);
                break;
            }
            if (box != this)
            {
                check = box.PointInBox(checkPosition) || box.PointInBox(dimensions + checkPosition) || box.PointInBox(new Vector2Int(dimensions.x, 0) + checkPosition) || box.PointInBox(new Vector2Int(0, dimensions.y) + checkPosition);
                if (check) break;
            }
        }
        return check;
    }
    public CollisionBox InstancePlace(int x_check, int y_check)
    {
        Vector2Int checkPosition = new Vector2Int(x_check, y_check);
        bool check = false;
        foreach (CollisionBox box in BoxSystem.boxList)
        {
            if (box == null)
            {
                BoxSystem.boxList.Remove(box);
                break;
            }
            if (box != this)
            {
                check = box.PointInBox(checkPosition) || box.PointInBox(dimensions + checkPosition) || box.PointInBox(new Vector2Int(dimensions.x, 0) + checkPosition) || box.PointInBox(new Vector2Int(0, dimensions.y) + checkPosition);
                if (check) return box;
            }
        }
        return null;
    }
    private void FixedUpdate()
    {
        transform.position = new Vector3Int(lockedPosition.x, lockedPosition.y);
        transform.localScale = lockedScale;
        //Debug.DrawLine(new Vector3(lockedPosition.x, lockedPosition.y), new Vector3(TopRight.x, TopRight.y), Color.red);
    }
    void Start()
    {
        BoxSystem.boxList.Add(this);
        
        lockedPosition.x = Mathf.RoundToInt(transform.position.x);
        lockedPosition.y = Mathf.RoundToInt(transform.position.y);
        lockedScale = new Vector3Int(Mathf.RoundToInt(transform.localScale.x), Mathf.RoundToInt(transform.localScale.y));
        transform.position = new Vector3(lockedPosition.x, lockedPosition.y);

        if (TryGetComponent(out SpriteRenderer renderer))
        {
            spriteRenderer = renderer;
            Bounds bounds = renderer.bounds;
            dimensions = new Vector2Int(Mathf.RoundToInt(bounds.size.x-1), Mathf.RoundToInt(bounds.size.y-1));
            topLeft = new Vector2Int(0, dimensions.y);
            bottomRight = new Vector2Int(dimensions.x, 0);
        }
        else
        {
            dimensions = new Vector2Int(lockedScale.x,lockedScale.y);
            topLeft = new Vector2Int(0, dimensions.y);
            bottomRight = new Vector2Int(dimensions.x, 0);
        }
    }
}
