using System.Collections.Generic;
using UnityEngine;

//TE ODIO
[ExecuteAlways]
public class FrustrumCulling : MonoBehaviour
{
    [SerializeField] private Vector2 aspectRatio;
    [SerializeField] private List<GameObject> gos;
    [SerializeField] private float fovY, zNear, zFar;
    public Frustrum Frustrum;

    private void Start()
    {
        Frustrum = new Frustrum(aspectRatio.x / aspectRatio.y, fovY, zNear, zFar);
    }

    private void Update()
    {
        UpdateFrustrum();

        foreach (GameObject go in gos)
        {
            bool found = true;
            foreach (var plane in Frustrum.GetPlanes())
            {
                if (!GetSide(plane, go.transform.position))
                {
                    found = false;
                    
                }
            }
            go.SetActive(found);
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