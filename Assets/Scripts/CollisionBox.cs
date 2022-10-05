using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionBox : MonoBehaviour
{
    [SerializeField]
    private Vector2Int dimensions;
    public Vector2Int lockedPosition;
    private Vector2Int topLeft, bottomRight;

    public Vector2Int Dimensions { get { return dimensions; } set { } }
    public Vector2Int TopLeft { get { return topLeft + lockedPosition; } set { } }
    public Vector2Int TopRight { get { return dimensions + lockedPosition; } set { } }
    public Vector2Int BottomLeft { get { return lockedPosition; } set { } }
    public Vector2Int BottomRight { get { return bottomRight + lockedPosition; } set { } }

    public SpriteRenderer spriteRenderer;
    public bool PointInBox(int x_check, int y_check)
    {
        return (x_check < lockedPosition.x + dimensions.x && x_check > lockedPosition.x && y_check < lockedPosition.x + dimensions.y && y_check > lockedPosition.y);
    }
    public bool PointInBox(Vector2Int checkPosition)
    {
        int x_check = checkPosition.x;
        int y_check = checkPosition.y;
        if (x_check < lockedPosition.x + dimensions.x && x_check > lockedPosition.x && y_check < lockedPosition.x + dimensions.y && y_check > lockedPosition.y)
        {
            return true;
        }
        else return false;
    }
    public bool PlaceMeeting(int x_check, int y_check, Func<CollisionBox, bool> qualifier)
    {
        Vector2Int checkPosition = new Vector2Int(x_check, y_check);
        bool check = false;
        foreach (CollisionBox box in BoxSystem.boxList)
        {
            if (box != this) {
                check = qualifier(box) && (PointInBox(box.BottomLeft + checkPosition) || PointInBox(box.TopLeft + checkPosition) || PointInBox(box.TopRight + checkPosition) || PointInBox(box.BottomRight + checkPosition));
                if (check) break;
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
            check = (PointInBox(box.BottomLeft + checkPosition) || PointInBox(box.TopLeft + checkPosition) || PointInBox(box.TopRight + checkPosition) || PointInBox(box.BottomRight + checkPosition));
            if (check) break;
        }
        return check;
    }
    public CollisionBox InstancePlace(int x_check, int y_check)
    {
        Vector2Int checkPosition = new Vector2Int(x_check, y_check);
        bool check = false;
        foreach (CollisionBox box in BoxSystem.boxList)
        {
            check = (PointInBox(box.BottomLeft + checkPosition) || PointInBox(box.TopLeft + checkPosition) || PointInBox(box.TopRight + checkPosition) || PointInBox(box.BottomRight + checkPosition));
            if (check) return box;
        }
        return null;
    }
    private void FixedUpdate()
    {
        transform.position = new Vector3Int(lockedPosition.x, lockedPosition.y);
    }
    void Start()
    {
        BoxSystem.boxList.Add(this);
        topLeft = new Vector2Int(0, dimensions.y);
        bottomRight = new Vector2Int(dimensions.x, 0);

        lockedPosition.x = Mathf.RoundToInt(transform.position.x);
        lockedPosition.y = Mathf.RoundToInt(transform.position.y);
        transform.position = new Vector3(lockedPosition.x, lockedPosition.y);

        if (TryGetComponent(out SpriteRenderer renderer))
        {
            spriteRenderer = renderer;
            Bounds bounds = renderer.bounds;
            lockedPosition = new Vector2Int(Mathf.RoundToInt(bounds.center.x - bounds.extents.x), Mathf.RoundToInt(bounds.center.y - bounds.extents.y));
            dimensions = new Vector2Int(Mathf.RoundToInt(bounds.size.x), Mathf.RoundToInt(bounds.size.y));
        }
        else
        {
            lockedPosition = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
            dimensions = new Vector2Int(Mathf.RoundToInt(transform.localScale.x), Mathf.RoundToInt(transform.localScale.y));
        }
    }
}
