using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FrustumDrawer : MonoBehaviour
{
    public FrustrumCulling frustrumCulling;

    private void OnDrawGizmos()
    {
        if (frustrumCulling.Frustrum != null)
            DrawFrustrum(frustrumCulling.Frustrum);
    }

    private Vector3 IntersectThreePlanes(MyPlane p1, MyPlane p2, MyPlane p3)
    {
        Vector3 n1 = p1.Normal, n2 = p2.Normal, n3 = p3.Normal;
        float determinant = MyTools.DotProduct(n1, MyTools.CrossProduct(n2, n3));

        if (Mathf.Abs(determinant) < 1e-6f)
        {
            Debug.LogWarning("Planes do not intersect at a single point.");
            return Vector3.zero;
        }

        Vector3 intersectPoint = (
            (-p1.Distance * MyTools.CrossProduct(n2, n3)) +
            (-p2.Distance * MyTools.CrossProduct(n3, n1)) +
            (-p3.Distance * MyTools.CrossProduct(n1, n2))
        ) / determinant;

        return intersectPoint;
    }

    private void DrawFrustrum(Frustrum frustrum) // Chat GPT ME AYUDO CON EL DIBUJADO
    {
        if (frustrum.GetPlanes().Length < 0) return;
        
        Vector3[] corners = new Vector3[8];
        corners[0] = IntersectThreePlanes(frustrum.leftFace, frustrum.topFace, frustrum.nearFace); // Near Top Left
        corners[1] = IntersectThreePlanes(frustrum.rightFace,frustrum.topFace, frustrum.nearFace); // Near Top Right
        corners[2] = IntersectThreePlanes(frustrum.rightFace, frustrum.bottomFace, frustrum.nearFace); // Near Bottom Right
        corners[3] = IntersectThreePlanes(frustrum.leftFace, frustrum.bottomFace, frustrum.nearFace); // Near Bottom Left
        corners[4] = IntersectThreePlanes(frustrum.leftFace,frustrum.topFace, frustrum.farFace); // Far Top Left
        corners[5] = IntersectThreePlanes(frustrum.rightFace, frustrum.topFace, frustrum.farFace); // Far Top Right
        corners[6] = IntersectThreePlanes(frustrum.rightFace, frustrum.bottomFace, frustrum.farFace); // Far Bottom Right
        corners[7] = IntersectThreePlanes(frustrum.leftFace, frustrum.bottomFace, frustrum.farFace); // Far Bottom Left

        Gizmos.color = Color.green;
        for (int i = 0; i < 4; i++)
        {
            Gizmos.DrawLine(corners[i], corners[(i + 1) % 4]);
            Gizmos.DrawLine(corners[i + 4], corners[(i + 1) % 4 + 4]);
            Gizmos.DrawLine(corners[i], corners[i + 4]);
        }
    }
}