using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ClickPlayer : MonoBehaviour, IPointerClickHandler
{
    private GameObject TargetObj;
    void Start()
    {
        TargetObj = GameObject.FindGameObjectWithTag("CameraSystem");
    }


    //public void FollowPlayer()
    //{
    //    TargetObj.transform.position = transform.position;
    //}

    //    float lastClick = 0f;
    //float interval = 0.4f;
    //    public void OnPointerClick(PointerEventData eventData)
    //    {
    //      if ((lastClick+interval)>Time.time)
    //          {//is a double click
    //            TargetObj.transform.position = transform.position;
    //        Debug.Log("Double Click");
    //        }
    //      else
    //          {//is a single click
    //            Debug.Log("Single Click");
    //           }
    //      lastClick = Time.time;
    //    }




    private float DoubleClickInterval = 0.5f;
    private float secondClickTimeout = -1;

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (secondClickTimeout < 0)
        {

          //  TargetObj.transform.position = transform.position;
            secondClickTimeout = DoubleClickInterval;

        }
        else
        {
           TargetObj.GetComponent<CameraSystem>().followPlayer = true;
            Debug.Log("Double Clicked!");
            secondClickTimeout = -1;
        }
    }

    void Update()
    {
        // Update the timer
        if (secondClickTimeout >= 0)
        {
            secondClickTimeout -= Time.deltaTime;
        }
    }


}
