using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


//Extends vectors for use in Bullet Trails
public static class VectorsExtension 
{
    public enum Axis
    {
        X, 
        Y, 
        Z
    }

    public static Vector3 WithAxis(this Vector3 vector, Axis axis, float value)
    {
        return new Vector3(
            x: axis == Axis.X ? value : vector.x,
            y: axis == Axis.Y ? value : vector.y,
            z: axis == Axis.Z ? value : vector.z
            );
    }
}
