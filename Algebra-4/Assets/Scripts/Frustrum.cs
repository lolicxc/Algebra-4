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


    public Frustrum(float aspect, float fovY, float zNear, float zFar)
    {
        Transform cam = Camera.main.transform;

        fovY *= Mathf.Deg2Rad;

        float halfVSide = zFar * Mathf.Tan(fovY * 0.5f);
        float halfHSide = halfVSide * aspect;
        
        Vector3 frontMultFar = zFar * Camera.main.transform.forward;

        nearFace = new MyPlane(cam.position + zNear * cam.forward, cam.forward);
        farFace = new MyPlane(cam.position + frontMultFar, -cam.forward);
        rightFace = new MyPlane(cam.position, Vector3.Cross(frontMultFar - cam.right * halfHSide, cam.up));

        leftFace = new MyPlane(cam.position, Vector3.Cross(cam.up, frontMultFar + cam.right * halfHSide));

        topFace = new MyPlane(cam.position, Vector3.Cross(cam.right, frontMultFar - cam.up * halfVSide));

        bottomFace = new MyPlane(cam.position, Vector3.Cross(frontMultFar + cam.up * halfVSide, cam.right));
    }

    public void SetData(float aspect, float fovY, float zNear, float zFar)
    {
        Transform cam = Camera.main.transform;

        fovY *= Mathf.Deg2Rad;

        float halfVSide = zFar * Mathf.Tan(fovY * 0.5f);
        float halfHSide = halfVSide * aspect;
        
        Vector3 frontMultFar = zFar * Camera.main.transform.forward;

        nearFace.SetNormalAndPosition(cam.position + zNear * cam.forward, cam.forward);
        farFace.SetNormalAndPosition(cam.position + frontMultFar, -cam.forward);
        rightFace.SetNormalAndPosition(cam.position, Vector3.Cross(frontMultFar - cam.right * halfHSide, cam.up));

        leftFace.SetNormalAndPosition(cam.position, Vector3.Cross(cam.up, frontMultFar + cam.right * halfHSide));

        topFace.SetNormalAndPosition(cam.position, Vector3.Cross(cam.right, frontMultFar - cam.up * halfVSide));

        bottomFace.SetNormalAndPosition(cam.position, Vector3.Cross(frontMultFar + cam.up * halfVSide, cam.right));
    }

    public MyPlane[] GetPlanes()
    {
        return new MyPlane[6] { nearFace, farFace, rightFace, leftFace, topFace, bottomFace };
    }
}