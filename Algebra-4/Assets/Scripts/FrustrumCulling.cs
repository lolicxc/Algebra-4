using System.Collections.Generic;
using UnityEngine;

//TE ODIO
[ExecuteAlways]
public class FrustrumCulling : MonoBehaviour
{
    [SerializeField] private Vector2 aspectRatio;
    [SerializeField] private List<AABB> gos;
    [SerializeField] private float fovY, zNear, zFar;
    public Frustrum Frustrum;

    private void Start()
    {
        Frustrum = new Frustrum(aspectRatio.x / aspectRatio.y, fovY, zNear, zFar);
    }

    private void Update()
    {
        UpdateFrustrum();

        foreach (AABB go in gos)
        {
            bool found = true;
            foreach (var plane in Frustrum.GetPlanes())
            {
                Vector3[] vars = go.BoxVertices;
                
                for (int i = 0; i < go.BoxVertices.Length; i++)
                {
                    if (!GetSide(plane, go.transform.position))
                    {
                        found = false;
                        break;
                    }
                }
                
            }

            go.GetComponent<MeshRenderer>().enabled = found;
        }
        
    }

    public bool GetSide(MyPlane plane, Vector3 position)
    {
        return plane.GetSide(position);
    }


    private void UpdateFrustrum()
    {
        Frustrum.SetData(aspectRatio.x / aspectRatio.y, fovY, zNear, zFar);
    }
}