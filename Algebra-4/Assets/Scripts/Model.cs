using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    public Vector3 center = Vector3.zero;
    public Vector3 extents = Vector3.zero;

    [SerializeField] MeshFilter mesh;

    [SerializeField] private Vector3 min, max;
    [SerializeField] private FrustrumCulling frustrumRef;
    private void Awake()
    {
        center = (max + min) * 0.5f;
        extents = new Vector3(max.x - center.x, max.y - center.y, max.z - center.z);
    }

    private void Update()
    {
        Debug.Log(IsOnFrustrum());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.TransformPoint(center), extents);
        Gizmos.color = Color.green;
    }

    public void SetAABB(Vector3 min , Vector3 max)
    {
        center = (max + min) * 0.5f;
        extents = new Vector3(max.x - center.x, max.y - center.y, max.z - center.z);
    }

    public void SetAABB(Vector3 inCenter, float iI, float iJ, float iK)
    {
        center = inCenter;
        extents = new Vector3(iI, iJ, iK);
    }

    public bool IsOnFrustrum()
    {
        Vector3 globalCenter = transform.position;

        Vector3 right = transform.right * extents.x;
        Vector3 up = transform.up * extents.y;
        Vector3 forward = transform.forward * extents.z;

        float newIi = Mathf.Abs(Vector3.Dot(Vector3.right, right)) +
                        Mathf.Abs(Vector3.Dot(Vector3.right, up)) +
                        Mathf.Abs(Vector3.Dot(Vector3.right, forward));

        float newIj = Mathf.Abs(Vector3.Dot(Vector3.up, right)) +
                        Mathf.Abs(Vector3.Dot(Vector3.up, up)) +
                        Mathf.Abs(Vector3.Dot(Vector3.up, forward));

        float newIk = Mathf.Abs(Vector3.Dot(Vector3.right, right)) +
                        Mathf.Abs(Vector3.Dot(Vector3.right, up)) +
                        Mathf.Abs(Vector3.Dot(Vector3.right, forward));

        return true;
        //AABB globalAABB = new AABB();
        //globalAABB.SetAABB(globalCenter, newIi, newIj, newIk);
        //
        //return (globalAABB.IsOnOrForwardPlane(frustrumRef.Frustrum.leftFace) &&
        //    globalAABB.IsOnOrForwardPlane(frustrumRef.Frustrum.rightFace) &&
        //    globalAABB.IsOnOrForwardPlane(frustrumRef.Frustrum.topFace) &&
        //    globalAABB.IsOnOrForwardPlane(frustrumRef.Frustrum.bottomFace) &&
        //    globalAABB.IsOnOrForwardPlane(frustrumRef.Frustrum.nearFace) &&
        //    globalAABB.IsOnOrForwardPlane(frustrumRef.Frustrum.farFace));
    }
}
