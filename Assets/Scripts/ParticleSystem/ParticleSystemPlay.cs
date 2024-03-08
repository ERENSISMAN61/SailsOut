using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemPlay : MonoBehaviour
{
    
    ParticleSystem smokeParticle;
    private void Start()
    {
        smokeParticle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (smokeParticle != null)
        {
            
            if (Input.GetMouseButton(1))
            {
                if(Input.GetMouseButtonDown(0))
                {
                   
                    smokeParticle.Play();
                }
            }
        }
    }
}
