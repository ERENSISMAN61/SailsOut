using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawProjection : MonoBehaviour
{
    PlayerFire cannonController;
    LineRenderer lineRenderer;
    public int numPoints = 75;

    public float timeBetweenPoints = 0.1f;
    public LayerMask CollidableLayers;
    // Start is called before the first frame update
    void Start()
    {
        cannonController = gameObject.GetComponent<PlayerFire>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lineRenderer != null)
        {
            if (Input.GetMouseButton(1))
            {
                DrawTrajectory();
                lineRenderer.enabled = true;
            }
            else
            {
                lineRenderer.enabled = false;
            }
        }
        
    }

    void DrawTrajectory()
    {
        //lineRenderer.positionCount = (int)numPoints;
        //List<Vector3> points = new List<Vector3>();
        //Vector3 startPosition = cannonController.firePoint.position;
        ////Vector3 startVelocity = cannonController.firePoint.up * cannonController.initialVelocity;
        //for (float t = 0; t < numPoints; t += timeBetweenPoints)
        //{
        //    Vector3 newPoint = startPosition + t * startVelocity;
        //    newPoint.y = startPosition.y + startVelocity.y * t + Mathf.Abs(Physics.gravity.y) / 2f * t * t;
        //    points.Add(newPoint);
        //    if (Physics.OverlapSphere(newPoint, 2f, CollidableLayers).Length > 0)
        //    {
        //        lineRenderer.positionCount = points.Count;
        //        break;
        //    }
        //}
        //lineRenderer.SetPositions(points.ToArray());
    }
}
