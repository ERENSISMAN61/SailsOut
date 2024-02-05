using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDestinationIsland : MonoBehaviour
{
    private Transform transformDock;
    [SerializeField] private Vector3 offset;
    void Start()
    {
        transformDock = gameObject.transform;


    }

    public void SetDestToDock()
    {
        GameObject.FindWithTag("Player").GetComponent<SmoothPlayerMovement>().SetDestinationPlus(transformDock.position+offset);
        Debug.Log("Destination set to: " + transformDock.position);
    }
}
