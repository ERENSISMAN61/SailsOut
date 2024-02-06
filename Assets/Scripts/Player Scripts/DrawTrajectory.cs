using Cinemachine;
using System.Collections;
using System.Collections.Generic;
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
    private Transform cannon;

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
        cannon = transform.parent;

        // Calculate the positions
        CalculatePositions();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the positions
        if(Input.GetMouseButton(1))
        {
            CalculatePositions();
        }
        
    }

    // Calculate the positions of the line renderer
    void CalculatePositions()
    {
        // Get the initial velocity
        float velocity = speed * transform.localScale.x;

        // Get the initial position
        Vector3 position = transform.position;

        // Get the initial direction
        Vector3 direction = Quaternion.AngleAxis(angle, cannon.right) * cannon.forward;

        // Loop through the segments
        for (int i = 0; i < segmentCount; i++)
        {
            // Set the position of the segment
            positions[i] = position;

            // Calculate the displacement
            Vector3 displacement = direction * velocity * Time.fixedDeltaTime + Vector3.up * gravity * Time.fixedDeltaTime * Time.fixedDeltaTime / 2f;

            // Update the position
            position += displacement;

            // Update the direction
            direction += Vector3.up * gravity * Time.fixedDeltaTime;
        }

        // Set the positions of the line renderer
        lineRenderer.SetPositions(positions);
    }
}


