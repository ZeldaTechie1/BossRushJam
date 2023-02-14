using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class HelperFunctions
{

    public enum Direction
    {
        Up, Down, Left, Right,
    };

    public static Vector3[] VectorDirections = new Vector3[] { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
    public static Vector2[] Vector2Directions = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector3.right };

    /// <summary>
    /// Gets a vector and finds the closes cardinal direction.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static Direction CardinalizeVector(Vector3 vector)
    {
        //directions to compare to, flexible with any amount of directions
        Vector3 cardinalDirection;

        float maxDot = -Mathf.Infinity;
        cardinalDirection = Vector3.zero;

        //finds which direction has the highest dot product which means it's closest to that direction
        foreach (Vector3 direction in VectorDirections)
        {
            float dotProduct = Vector3.Dot(vector, direction);
            if (dotProduct > maxDot)
            {
                maxDot = dotProduct;
                cardinalDirection = direction;
            }
        }
        return (Direction)VectorDirections.ToList().IndexOf(cardinalDirection);
    }

    public static Direction CardinalizeVector(Vector2 vector)
    {
        //directions to compare to, flexible with any amount of directions
        Vector3 cardinalDirection;

        float maxDot = -Mathf.Infinity;
        cardinalDirection = Vector2.zero;

        //finds which direction has the highest dot product which means it's closest to that direction
        foreach (Vector2 direction in Vector2Directions)
        {
            float dotProduct = Vector2.Dot(vector, direction);
            if (dotProduct > maxDot)
            {
                maxDot = dotProduct;
                cardinalDirection = direction;
            }
        }
        return (Direction)Vector2Directions.ToList().IndexOf(cardinalDirection);
    }

}
