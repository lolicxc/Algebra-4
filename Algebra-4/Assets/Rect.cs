using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Rect
{
    public UnityEngine.Vector3 startPos;
    public UnityEngine.Vector3 finishPos;
    //vector director
    public UnityEngine.Vector3 rotationAngles;
    public float magnitude;

    public Rect(UnityEngine.Vector3 startPos, UnityEngine.Vector3 rotationAngles, float magnitude)
    {
        this.startPos = startPos;
        this.rotationAngles = rotationAngles;
        this.magnitude = magnitude;

        this.finishPos = GetFinishPos();
    }

    public UnityEngine.Vector3 GetFinishPos()
    {
        UnityEngine.Vector3 finishPos;

        finishPos.x = this.startPos.x + this.magnitude * Mathf.Cos(rotationAngles.y)/* * Mathf.Cos(rotationAngles.z)*/;
        finishPos.y = this.startPos.y + this.magnitude * Mathf.Sin(rotationAngles.x)/* * Mathf.Cos(rotationAngles.y)*/;
        finishPos.z = this.startPos.z; //+ this.magnitude * Mathf.Sin(rotationAngles.z);

        return finishPos;
    }
}
