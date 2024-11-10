using UnityEngine;
//TE ODIO
[ExecuteAlways]
public class FrustrumCulling : MonoBehaviour
{
    [SerializeField] private Vector2 aspectRatio;

    [SerializeField] private float fovY, zNear, zFar;
    public Frustrum Frustrum;

    private void Start()
    {
        Frustrum = new Frustrum(aspectRatio.x / aspectRatio.y, fovY, zNear, zFar);
    }

    private void Update()
    {
        UpdateFrustrum();   
    }

    private void UpdateFrustrum()
    {
        Frustrum.SetData(aspectRatio.x / aspectRatio.y, fovY, zNear, zFar);
    }

    


}
