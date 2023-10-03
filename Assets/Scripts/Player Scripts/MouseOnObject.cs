using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;


public class MouseOnObject : MonoBehaviour
{
   // HDAdditionalCameraData additionalCameraData;
  //  int layerNum;
    void Start()
    {
   //     additionalCameraData = GameObject.FindWithTag("MainCamera").GetComponent<HDAdditionalCameraData>();
     //    layerNum = LayerMask.NameToLayer("OutlineClick");
    }
    private void OnMouseEnter()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().gameObject.layer = LayerMask.NameToLayer("OutlineTrue");
     //   additionalCameraData.volumeLayerMask |= (1 << layerNum);
    }

    private void OnMouseExit()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().gameObject.layer = LayerMask.NameToLayer("OutlineFalse");
        //   additionalCameraData.volumeLayerMask &= ~(1 << layerNum);

    }
}
