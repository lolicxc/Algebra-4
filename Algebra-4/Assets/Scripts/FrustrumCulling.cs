using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class FrustrumCulling : MonoBehaviour
{
    [SerializeField] private Vector2 aspectRatio;
    [SerializeField] private List<AABB> gos;
    [SerializeField] private float fovY, zNear, zFar;
    public Frustrum Frustrum;

    private void Start()
    {
        Frustrum = new Frustrum(transform, aspectRatio.x / aspectRatio.y, fovY, zNear, zFar);
    }

    private void Update()
    {
        UpdateFrustrum();

        foreach (AABB go in gos)
        {
            go.GetComponent<MeshRenderer>().enabled = IsPointOnPlane(go);
        }
    }

    private bool IsPointOnPlane(AABB go)
    {
        Vector3[] vertices = go.BoxVertices;

        

        for (int i = 0; i < vertices.Length; i++)
        {
            int count = 0;

            foreach (var item in Frustrum.GetPlanes())
            {
                if (GetSide(item, vertices[i]))
                {
                    count++;
                }
            }

            if (count == 6)
            {
                return true;
            }
        }

        return false;
    }

    public bool GetSide(MyPlane plane, Vector3 position)
    {
        return plane.GetSide(position);
    }


    private void UpdateFrustrum()
    {
        Frustrum.SetData(transform, aspectRatio.x / aspectRatio.y, fovY, zNear, zFar);
    }
}