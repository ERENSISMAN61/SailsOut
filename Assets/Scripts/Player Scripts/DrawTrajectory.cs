using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{

    // The number of points on the line
    public int segmentCount = 20;

    // The speed of the projectile
    public float speed = 10f;

    // The gravity of the projectile
    public float gravity = -9.81f;

    // The initial angle of the projectile
    public float angle = 45f;

    // The line renderer component
    private LineRenderer lineRenderer;

    // The positions array
    public Vector3[] positions;

    // The cannon transform
    private Transform rightPoint;
    private Transform leftPoint;

    public CinemachineVirtualCamera rightCamera;
    public CinemachineVirtualCamera leftCamera;

    public float timeOfFlight = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the line renderer component
        lineRenderer = GetComponent<LineRenderer>();    

        // Set the segment count
        lineRenderer.positionCount = segmentCount;

        // Initialize the positions array
        positions = new Vector3[segmentCount];

        // Find the cannon transform
        rightPoint = GameObject.FindGameObjectWithTag("RightTrajectory").GetComponent<Transform>().transform;
        leftPoint = GameObject.FindGameObjectWithTag("LeftTrajectory").GetComponent<Transform>().transform;

        // Calculate the positions
        CalculatePositions();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the positions
        if(Input.GetMouseButton(1))
        {
            lineRenderer.enabled = true;
            CalculatePositions();
        }
        else
        {
            lineRenderer.enabled = false;
        }
        
    }

    // Calculate the positions of the line renderer
    void CalculatePositions()
    {
        // Get the initial velocity
        float velocity = 0;

        // Get the initial position
        Vector3 position = new Vector3();
        // Get the initial direction
        Vector3 direction = new Vector3();
        if (rightCamera.Priority > leftCamera.Priority)
        {
            velocity = speed * rightPoint.localScale.x;
            position = rightPoint.position;
            direction = Quaternion.AngleAxis(angle, rightPoint.forward) * rightPoint.forward;
        }
        else
        {
            velocity = speed * leftPoint.localScale.x;
            position = leftPoint.position;
            Vector3 leftVector = -rightPoint.forward;
            direction = Quaternion.AngleAxis(angle, leftPoint.forward) * leftVector;
        }
        

        // Loop through the segments
        for (int i = 0; i < segmentCount; i++)
        {
            // Set the position of the segment
            positions[i] = position;

            // Calculate the displacement
            Vector3 displacement = direction * velocity * timeOfFlight + Vector3.up * Physics.gravity.y * timeOfFlight * timeOfFlight / 2f;

            // Update the position
            position += displacement;

            // Update the direction
            direction += Vector3.up * Physics.gravity.y * timeOfFlight;
        }

        // Set the positions of the line renderer
        lineRenderer.SetPositions(positions);
    }
}


