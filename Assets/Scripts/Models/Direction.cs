using UnityEngine;

public enum Direction
{
    left,
    right,
    down,
    up,
}

static class DirectionMethods
{
    public static Direction GetOpposite(this Direction direction)
    {
        switch (direction)
        {
            case Direction.left:
                return Direction.right;
            case Direction.right:
                return Direction.left;
            case Direction.up:
                return Direction.down;
            case Direction.down:
                return Direction.up;
            default:
                return direction;
        }
    }

    public static Vector3 GetVector3(this Direction direction)
    {
        switch (direction)
        {
            case Direction.left:
                return Vector3.left;
            case Direction.right:
                return Vector3.right;
            case Direction.up:
                return Vector3.forward;
            case Direction.down:
                return Vector3.back;
            default:
                return Vector3.zero;
        }
    }
}
