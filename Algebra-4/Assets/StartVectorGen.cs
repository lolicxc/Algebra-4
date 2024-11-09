using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class StartVectorGen : MonoBehaviour
{
    [SerializeField] private Transform pointA;

    [SerializeField] private Vector2 aspectRatio;

    [SerializeField] private float hFOV;
    [SerializeField] private float vFOV;

    private GameObject camera;

    private Vector3 bottomLeft;
    private Vector3 bottomRight;
    private Vector3 topRight;
    private Vector3 topLeft;

    private float nearDist;

    private bool nearDone = false;

    private float cameraHeight;

    private float cameraWidth;

    private Vector3 startPoint;
    Vector3 center;

    private float vectorAngle;
    private float FOD;

    private List<Rect> vectors;
    private List<Vector3> vectorEnds;

    int currentVector;

    [ContextMenu("Gen")]

    Vector3 MultiplyVectorNum(Vector3 vector, float num)
    {
        return new Vector3(vector.x * num, vector.y * num, vector.z * num);
    }

    Vector3 SubtractVectors(Vector3 vector1, Vector3 vector2)
    {
        return new Vector3(vector1.x - vector2.x, vector1.y - vector2.y, vector1.z - vector2.z);
    }

    Vector3 AddVectors(Vector3 vector1, Vector3 vector2)
    {
        return new Vector3(vector1.x + vector2.x, vector1.y + vector2.y, vector1.z + vector2.z);
    }

    Vector3 AddVectorNum(Vector3 vector1, float num)
    {
        return new Vector3(vector1.x + num, vector1.y + num, vector1.z + num);
    }

    public void GenNearPlane()
    {
        center = camera.transform.position;

        bottomLeft = center;

        Vector3 horizontalScale = MultiplyVectorNum(-camera.transform.right, cameraWidth / 2);
        Vector3 verticalScale = MultiplyVectorNum(-camera.transform.up, cameraHeight / 2);

        Vector3 scale = AddVectors(horizontalScale, verticalScale);

        bottomLeft = AddVectors(bottomLeft, scale); ;

        bottomRight = new Vector3(bottomLeft.x + aspectRatio.x, bottomLeft.y, bottomLeft.z);
        topRight = new Vector3(bottomRight.x, bottomRight.y + aspectRatio.y, bottomRight.z);
        topLeft = new Vector3(topRight.x - aspectRatio.x, topRight.y, topRight.z);

        nearDone = true;
    }



    public void SetUpStartPoint()
    {
        //adyacente con sohcahtoa
        float radFOV = (Mathf.PI / 180.0f) * hFOV;

        Debug.Log(Mathf.Tan(radFOV / 2));
        nearDist = (cameraWidth / 2) / Mathf.Tan(radFOV / 2);

        startPoint = center;

        Vector3 scaledStart = new Vector3();

        scaledStart = MultiplyVectorNum(camera.transform.forward, -nearDist/**2 + 7*/);
        Debug.Log(nearDist);
        Debug.Log(scaledStart.magnitude);
        startPoint = AddVectors(startPoint, scaledStart);

        camera.transform.position = startPoint;

    }

    public void SetVectors()
    {

        //float hRadFOV = (Mathf.PI / 180.0f) * hFOV;
        //float vRadFOV = (Mathf.PI / 180.0f) * vFOV;

        ////Hyp = opuesto / seno(theta)

        //float verHypotenuse = cameraHeight / 2 / Mathf.Sin(vRadFOV / 2);
        //float horHypotenuse = cameraWidth / 2 / Mathf.Sin(hRadFOV / 2);

        //float verOpposite = verHypotenuse / Mathf.Tan(vRadFOV / 2);
        //float horOpposite = horHypotenuse / Mathf.Tan(hRadFOV / 2);

        // Vector superior derecho
        Vector3 topRightVector = new Vector3(
            cameraWidth / 2,  // x
            cameraHeight / 2,  // y
            nearDist    // z
        );

        // Vector inferior derecho
        Vector3 bottomRightVector = new Vector3(
            cameraWidth / 2,    // -x
            -cameraHeight / 2,   // -y
            nearDist      // z
        );

        // Vector inferior izquierdo
        Vector3 bottomLeftVector = new Vector3(
            -cameraWidth / 2,   // x
            -cameraHeight / 2,   // -y
            nearDist      // z
        );

        // Vector superior izquierdo
        Vector3 topLeftVector = new Vector3(
            -cameraWidth / 2,  // -x
            cameraHeight / 2,   // y
            nearDist     // z
        );

        // A adir los vectores a la lista
        vectors.Add(new Rect(startPoint, topRightVector, FOD));
        vectors.Add(new Rect(startPoint, topLeftVector, FOD));
        vectors.Add(new Rect(startPoint, bottomRightVector, FOD));
        vectors.Add(new Rect(startPoint, bottomLeftVector, FOD));

        // Calcular y guardar los puntos finales de los vectores
        vectorEnds = new List<Vector3>();

        foreach (Rect vector in vectors)
        {
            Vector3 endPoint = vector.startPos + MultiplyVectorNum(vector.rotationAngles, vector.magnitude);
            vectorEnds.Add(endPoint);
        }

    }

    // Update is called once per frame
    void Awake()
    {
        camera = GameObject.Find("Main Camera");

        vectors = new List<Rect>();

        //hardcoded. See actual values
        hFOV = 160;
        vFOV = 90;
        cameraHeight = aspectRatio.y;
        cameraWidth = aspectRatio.x;

        currentVector = 0;
        FOD = 20;

        GenNearPlane();
        SetUpStartPoint();
        SetVectors();
    }

    private void OnDrawGizmos()
    {
        if (nearDone)
        {

            Gizmos.color = Color.red;
            Gizmos.DrawLine(topLeft, bottomLeft);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(topRight, topLeft);
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(bottomLeft, bottomRight);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(topRight, bottomRight);

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(startPoint, 0.2f);

            foreach (Rect vector in vectors)
            {
                Gizmos.DrawRay(vector.startPos, MultiplyVectorNum(vector.rotationAngles, vector.magnitude));
            }


            if (vectorEnds.Count == 4)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(vectorEnds[0], vectorEnds[1]);
                Gizmos.DrawLine(vectorEnds[1], vectorEnds[3]);
                Gizmos.DrawLine(vectorEnds[3], vectorEnds[2]);
                Gizmos.DrawLine(vectorEnds[2], vectorEnds[0]);
            }
        }
    }
}
