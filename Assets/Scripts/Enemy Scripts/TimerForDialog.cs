using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerForDialog : MonoBehaviour
{
    public float targetTimeScale = 0.1f;
    public float duration = 2.0f;
    private float elapsed = 0.0f;
    private bool slowingDown = false;
    private bool speedingUp = false;
    private GameObject Player;
    private Vector3 playerPos;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

    }
    private void Update()
    {

        Debug.Log("distance:  " + Vector3.Distance(playerPos, gameObject.transform.position));
        if (gameObject.GetComponent<SmoothAgentMovement>().isTargetEnemy && gameObject.GetComponent<SmoothAgentMovement>().didCatch)
        {
            playerPos = Player.transform.position;

            speedingUp = false;
            slowingDown = true;

        }
        else
        {

            slowingDown = false;
            speedingUp = true;

        }


        Debug.Log("isTargetEnemy: " + gameObject.GetComponent<SmoothAgentMovement>().isTargetEnemy + "     didcatch: " + gameObject.GetComponent<SmoothAgentMovement>().didCatch + "\nspeedingUp:  " + speedingUp + "     slowingDown:  " + slowingDown);

        if (speedingUp)
        {
            SpeedingUp();
        }
        if (slowingDown)
        {
            SlowDown();
        }


    }

    private void SlowDown()
    {
        Time.timeScale = 0f; // Time.timeScale = 0.3f;
        //  Time.fixedDeltaTime = Time.timeScale * 0.02f;



        //elapsed += Time.unscaledDeltaTime;
        //float progress = elapsed / duration;
        //Time.timeScale = Mathf.Lerp(1.0f, targetTimeScale, progress);
        //Time.fixedDeltaTime = Time.timeScale * 0.02f;
        //if (progress >= 1.0f)
        //{
        //    slowingDown = false;
        //    elapsed = 0.0f;
        //}
    }


    private void SpeedingUp()
    {

        Time.timeScale = 1f;
        // Time.fixedDeltaTime = Time.timeScale * 0.02f;

        //elapsed += Time.unscaledDeltaTime;
        //float progress = elapsed / duration;
        //Time.timeScale = Mathf.Lerp(targetTimeScale, 1.0f, progress);
        //Time.fixedDeltaTime = Time.timeScale * 0.02f;
        //if (progress >= 1.0f)
        //{
        //    speedingUp = false;
        //    elapsed = 0.0f;
        //}
    }







}
