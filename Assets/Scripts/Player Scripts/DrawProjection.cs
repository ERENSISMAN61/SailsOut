using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(LineRenderer))]
public class DrawProjection : MonoBehaviour
{
    

    //// The number of points on the line
    //public int segmentCount = 20;

    //public float timeOfFlight = 5f;

    //// The line renderer component
    //public LineRenderer lineRenderer;

    //// The positions array
    //public Vector3[] positions;

    //public CinemachineVirtualCamera rightCamera;
    //public CinemachineVirtualCamera leftCamera;


    //public void ShowTrajectory(Vector3 startPoint, float speed, float angle)
    //{
    //    // Convert the angle from degrees to radians
    //    float radianAngle = angle * Mathf.Deg2Rad;

    //    // Calculate the x and y components of the start velocity
    //    float xVelocity = Mathf.Cos(radianAngle) * speed;
    //    float yVelocity = Mathf.Sin(radianAngle) * speed;

    //    // Create a vector for the start velocity
    //    Vector3 startVelocity = new Vector3(xVelocity, yVelocity, 0);

    //    // The rest of the code is the same as before
    //    float timeStep = timeOfFlight / segmentCount;
    //    Vector3[] lineRendererPoints = CalculateTrajectoryLine(startPoint, startVelocity, timeStep);
    //    lineRenderer.positionCount = segmentCount;
    //    lineRenderer.SetPositions(lineRendererPoints);
    //}

    //public Vector3[] CalculateTrajectoryLine(Vector3 startPoint, Vector3 startVelocity, float timeStep)
    //{
    //    Vector3[] lineRendererPoints = new Vector3[segmentCount];


    //    lineRendererPoints[0] = startPoint;

    //    for (int i = 0; i < segmentCount; i++)
    //    {
    //        float timeOffset = i * timeStep;
    //        Vector3 ProgressBeforeGravity = startVelocity * timeOffset;
    //        Vector3 gravityOffset = Vector3.up * 0.5f * Mathf.Abs(Physics.gravity.y) * timeOffset * timeOffset;
    //        Vector3 newPosition = startPoint + ProgressBeforeGravity - gravityOffset;
    //        lineRendererPoints[i] = newPosition;

    //    }

    //    return lineRendererPoints;
    //}

    //private void Update()
    //{
    //    if (Input.GetMouseButton(1))
    //    {
    //        lineRenderer.enabled = true;
    //    }
    //    else
    //    {
    //        lineRenderer.enabled = false;
    //    }
    //}
}
