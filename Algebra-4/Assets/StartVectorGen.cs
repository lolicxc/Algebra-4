using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartVectorGen : MonoBehaviour
{
    [SerializeField] private Transform pointA;

    [SerializeField] private Vector2 aspectRatio;

    [SerializeField] private float verticalFOV;

    private Vector3 bottomLeft;
    private Vector3 bottomRight;
    private Vector3 topRight;
    private Vector3 topLeft;

    private bool done = false;

    [ContextMenu("Gen")]
    public void GenNearPlane()
    {
        Vector3 a = pointA.position;
        bottomLeft = a;

        bottomRight = new Vector3(bottomLeft.x + aspectRatio.x, bottomLeft.y, bottomLeft.z);
        topRight = new Vector3(bottomRight.x, bottomRight.y + aspectRatio.y, bottomRight.z);
        topLeft = new Vector3(topRight.x - aspectRatio.x, topRight.y, topRight.z);

        done = true;
    }

    // Update is called once per frame
    void Awake()
    {
        GenNearPlane();
    }

    private void OnDrawGizmos()
    {
        if (done)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(bottomLeft, bottomRight);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(bottomRight, topRight - bottomRight);
            Gizmos.color = Color.white;
            Gizmos.DrawRay(topRight, topLeft - topRight);
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(topLeft, bottomLeft- topLeft);
        }
    }
}
