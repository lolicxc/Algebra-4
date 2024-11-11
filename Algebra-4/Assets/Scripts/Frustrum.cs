using System;
using UnityEngine;

[Serializable]
public class Frustrum
{
    public MyPlane topFace;
    public MyPlane bottomFace;

    public MyPlane rightFace;
    public MyPlane leftFace;

    public MyPlane farFace;
    public MyPlane nearFace;


    public Frustrum(Transform transform, float aspect, float fovY, float zNear, float zFar)
    {
        SetData(transform, aspect, fovY, zNear, zFar);
    }

    public void SetData(Transform testTransform, float aspect, float fovY, float zNear, float zFar)
    {
        Transform cam = testTransform;

        fovY *= Mathf.Deg2Rad;

        float halfVSide = zFar * Mathf.Tan(fovY * 0.5f);
        float halfHSide = halfVSide * aspect;

        Vector3 frontMultFar = zFar * testTransform.forward;

        nearFace.SetNormalAndPosition(cam.position + zNear * cam.forward, -cam.forward);
        farFace.SetNormalAndPosition(cam.position + frontMultFar, cam.forward);
        rightFace.SetNormalAndPosition(cam.position, MyTools.CrossProduct(cam.up, frontMultFar + cam.right * halfHSide));

        leftFace.SetNormalAndPosition(cam.position, MyTools.CrossProduct(frontMultFar - cam.right * halfHSide, cam.up));

        topFace.SetNormalAndPosition(cam.position, MyTools.CrossProduct(cam.right, frontMultFar - cam.up * halfVSide));

        bottomFace.SetNormalAndPosition(cam.position, MyTools.CrossProduct(frontMultFar + cam.up * halfVSide, cam.right));
    }

    public MyPlane[] GetPlanes()
    {
        return new MyPlane[6] { nearFace, farFace, rightFace, leftFace, topFace, bottomFace };
    }
}