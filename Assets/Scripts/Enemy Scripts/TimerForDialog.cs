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

        Debug.Log("distance:  "+Vector3.Distance(playerPos, gameObject.transform.position));
        if (gameObject.GetComponent<SmoothAgentMovement>().isTargetEnemy)
        {
            playerPos = Player.transform.position;
            if (Vector3.Distance(playerPos, gameObject.transform.position) <= 200f)
            {
                speedingUp = false;
                slowingDown = true;

            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                slowingDown = false;
                speedingUp = true;

            }

        }

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
        elapsed += Time.unscaledDeltaTime;
        float progress = elapsed / duration;
        Time.timeScale = Mathf.Lerp(1.0f, targetTimeScale, progress);
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        if (progress >= 1.0f)
        {
            slowingDown = false;
            elapsed = 0.0f;
        }
    }


    private void SpeedingUp()
    {
        elapsed += Time.unscaledDeltaTime;
        float progress = elapsed / duration;
        Time.timeScale = Mathf.Lerp(targetTimeScale, 1.0f, progress);
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        if (progress >= 1.0f)
        {
            speedingUp = false;
            elapsed = 0.0f;
        }
    }







}
