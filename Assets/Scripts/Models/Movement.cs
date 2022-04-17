using System;
using UnityEngine;

public class Movement
{
    public Direction direction;
    public GameObject gameObject;

    public Movement(Direction direction)
    {
        this.direction = direction;
    }

    public Movement(Direction direction, GameObject gameObject)
    {
        this.direction = direction;
        this.gameObject = gameObject;
    }

    public override string ToString()
    {
        return this.direction +
            ((this.gameObject != null) ? $"[{this.gameObject.transform.name}]" : "");
    }
}
