using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ActorSolid
{
    public static class Actortracker
    {
        public static List<Actor> actorList = new List<Actor>();
    }
    public class Actorsolid : MonoBehaviour
    {
        CollisionBox box;
        protected int x = 0;
        protected int y = 0;
        protected Vector2 origin = new Vector2Int();

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
    static class BoxSystem
    {
        public static List<CollisionBox> boxList = new List<CollisionBox>();

    }
    public class CollisionBox : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int dimensions;
        private Vector2Int lockedPosition;
        private Vector2Int topLeft, bottomRight;

            public Vector2Int Dimensions { get { return dimensions; } set { } }
            public Vector2Int LockedPosition { get { return lockedPosition; } set { } }
            public Vector2Int TopLeft { get { return topLeft + lockedPosition; } set { } }
            public Vector2Int TopRight { get { return dimensions + lockedPosition; } set { } }
            public Vector2Int BottomLeft { get { return lockedPosition; } set { } }
            public Vector2Int BottomRight { get { return bottomRight + lockedPosition; } set { } }

        bool PointInBox(int x_check, int y_check)
        {
            if (x_check < lockedPosition.x + dimensions.x && x_check > lockedPosition.x && y_check < lockedPosition.x + dimensions.y && y_check > lockedPosition.y)
            {
                return true;
            }
            else return false;
        }
        bool PointInBox(Vector2Int checkPosition)
        {
            int x_check = checkPosition.x;
            int y_check = checkPosition.y;
            if (x_check < lockedPosition.x + dimensions.x && x_check > lockedPosition.x && y_check < lockedPosition.x + dimensions.y && y_check > lockedPosition.y)
            {
                return true;
            }
            else return false;
        }
        bool PlaceMeeting(int x_check, int y_check)
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
        void Start()
        {
            BoxSystem.boxList.Add(this);
            topLeft = new Vector2Int(0, dimensions.y);
            bottomRight = new Vector2Int(dimensions.x, 0);
        }
    }

    [RequireComponent(typeof(CollisionBox))]
    public class Actor : Actorsolid
    {
        Vector2 velocity;
        float drag = 0.01f;

        public delegate void CollisionAction();
        CollisionAction defaultAction;
        CollisionAction squishAction;

        void Start()
        {
            squishAction = Squish;

            StartPosition();
            box = GetComponent<Collider2D>();
            Actortracker.actorList.Add(this);
        }

        void FixedUpdate()
        {
            FixPosition();
        }

        bool CollideCheck(int x_check, int y_check, Solid solid)
        {

        }
        bool CollideCheck(int x_check, int y_check)
        {

        }

        void Squish()
        {

        }
        public void MoveX(float distance, CollisionAction action)
        {
            float remainder = distance;
            int move = Mathf.RoundToInt(distance);
            int direction = Math.Sign(move);

            while (move != 0)
            {
                if (CollideCheck(x + direction, y))
                {

                }
            }
        }
        public void MoveY(float distance, CollisionAction action)
        {
            float remainder = distance;
            int move = Mathf.RoundToInt(distance);
            int direciton = Math.Sign(move);


        }
    }
    [RequireComponent(typeof(CollisionBox))]
    public class Solid : Actorsolid
    {
        private bool collidable = false;
        public CollisionBox box;

        void Start()
        {
            box = GetComponent<CollisionBox>();
        }

        
        void Update()
        {

        }
        public void Move(float x_distance, float y_distance)
        {

        }
    }
}