using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperFunctions
{

    /// <summary>
    /// Gets a vector and finds the closes cardinal direction.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static Vector3 CardinalizeVector(Vector3 vector)
    {
        //directions to compare to, flexible with any amount of directions
        Vector3[] directions = new Vector3[] { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        Vector3 cardinalDirection;

        float maxDot = -Mathf.Infinity;
        cardinalDirection = Vector3.zero;

        //finds which direction has the highest dot product which means it's closest to that direction
        foreach (Vector3 direction in directions)
        {
            float dotProduct = Vector3.Dot(vector, direction);
            if (dotProduct > maxDot)
            {
                maxDot = dotProduct;
                cardinalDirection = direction;
            }
        }
        return cardinalDirection;
    }

}
