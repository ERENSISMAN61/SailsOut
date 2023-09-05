using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Floater : MonoBehaviour
{
    public Rigidbody rb;
    public float depthBeforeSubmerge = 1f;
    public float displacementAmount = 2f;
    public int floaters = 1;

    public float waterDrag = 2f;
    public float waterAngularDrag = 2f;
    public WaterSurface water;
    WaterSearchParameters search = new WaterSearchParameters();
    WaterSearchResult searchResult;
    private void FixedUpdate()
    {
        // Apply gravity to the rigidbody at multiple positions for stability.
        rb.AddForceAtPosition(Physics.gravity / floaters, transform.position, ForceMode.Acceleration);

        // Create water search parameters and get the water surface height.

        search.startPositionWS = transform.position;


        // Check if the object is below the water surface.
        if (transform.position.y < searchResult.candidateLocationWS.x)
        {
            // Calculate the displacement multiplier based on depth.
            float displacementMultiplier = Mathf.Clamp01((searchResult.projectedPositionWS.x - transform.position.y) / depthBeforeSubmerge) * displacementAmount;

            // Apply buoyant force.
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);

            // Apply drag forces.
            rb.AddForce(displacementMultiplier * -rb.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rb.AddTorque(displacementMultiplier * -rb.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }

        // Resetting the rotation might not be necessary, so you can remove this line.
        //gameObject.transform.rotation = Quaternion.identity;
    }
}
