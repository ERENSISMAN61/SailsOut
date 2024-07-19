using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DissolveAnimate : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private float speed = 0.2f;
    private float timer = 0;

    private bool canAnimate = false;

    public bool canDissolve = false;
    private bool singleTime = true;

    private bool canChange0, canChange1, canChange2, canChange3, canChange4 = false;
    private GameObject player;
    Vector3 playerPosition;
    private Material[] Mat;
    private string cutoffHeightPropertyName = "_CutOff_Height";
    private float increaseSpeed = 4f;
    private bool canChangeBool = false;

    private float duration = 40f;
    private float elapsedTime;
    private bool isChanging = false;

    private bool isEnterField = false;
    private bool isExitField = false;

    private float dotProductX, dotProductZ;

    private float newValue;

    private bool isEnteringAnimationComplete = true;
    private bool isExitingAnimationComplete = true;

    private bool isStartEnter = true;

    private float t;
    float currentValue;
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        gameObject.GetComponent<Renderer>().enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;

        if (canDissolve)
        {
            CalculateDotProduct();
        }




        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    canAnimate = true;
        //}
        //if ((canAnimate))
        //{
        //    Material[] materials = meshRenderer.materials;
        //    materials[0].SetFloat("_CutOff_Height", Mathf.Sin(timer * speed));
        //    materials[1].SetFloat("_CutOff_Height", Mathf.Sin(timer * speed));
        //    materials[2].SetFloat("_CutOff_Height", Mathf.Sin(timer * speed));
        //    timer += Time.deltaTime;

        //    meshRenderer.materials = materials;

        //    if (timer * speed >=12)
        //    {
        //        canAnimate = false;
        //        timer = 0;
        //    }
        //}

    }

    public void EnterOverlapSphere()
    {

        canDissolve = true;
        isEnterField = true;
        isExitField = false;
        // isChanging = true;
        isChanging = false;
        elapsedTime = 0f;
    }

    public void ExitOverlapSphere()
    {

        canDissolve = true;
        isExitField = true;
        isEnterField = false;
        isChanging = true;
        //isChanging = false;
        elapsedTime = 0f;
    }


    void CalculateDotProduct()
    {
        Vector3 directionToEnemy = playerPosition - transform.parent.position;
        Vector3 normalizedDirectionToEnemy = directionToEnemy.normalized;

        // X vekt�r� i�in dot product
        dotProductX = -(Vector3.Dot(transform.parent.right, normalizedDirectionToEnemy));
        // Z vekt�r� i�in dot product
        dotProductZ = -(Vector3.Dot(transform.parent.forward, normalizedDirectionToEnemy));



        Mat = GetComponent<Renderer>().materials;

        Mat[0].SetVector("_SplitVector", new Vector3(dotProductX, 0, dotProductZ));
        Mat[1].SetVector("_SplitVector", new Vector3(dotProductX, 0, dotProductZ));
        Mat[2].SetVector("_SplitVector", new Vector3(dotProductX, 0, dotProductZ));





        float startValue, endValue;

        float angle = Mathf.Atan2(dotProductZ, dotProductX) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        Debug.Log("Angle:     " + angle);

        if (angle >= 0 && angle < 45)
        {
            t = angle / 45;
            startValue = Mathf.Lerp(-36, -30, t);
            endValue = Mathf.Lerp(-22, -7, t);
        }
        if (angle >= 45 && angle < 90)
        {
            t = (angle - 45) / 45;
            startValue = Mathf.Lerp(-30, -12, t);
            endValue = Mathf.Lerp(-7, 18, t);
        }
        else if (angle >= 90 && angle < 135)
        {
            t = (angle - 90) / 45;
            startValue = Mathf.Lerp(-12, 10, t);
            endValue = Mathf.Lerp(19, 34, t);
        }
        else if (angle >= 135 && angle < 180)
        {
            t = (angle - 135) / 45;
            startValue = Mathf.Lerp(10, 22, t);
            endValue = Mathf.Lerp(35, 36, t);
        }
        else if (angle >= 180 && angle < 225)
        {
            t = (angle - 180) / 45;
            startValue = Mathf.Lerp(21, 5, t);
            endValue = Mathf.Lerp(37, 33, t);
        }
        else if (angle >= 225 && angle < 270)
        {
            t = (angle - 225) / 45;
            startValue = Mathf.Lerp(5, -20, t);
            endValue = Mathf.Lerp(30, 14, t);
        }
        else if (angle >= 270 && angle < 315)
        {
            t = (angle - 270) / 45;
            startValue = Mathf.Lerp(-22, -37, t);
            endValue = Mathf.Lerp(13, -9, t);
        }
        else
        {
            t = (angle - 315) / 45;
            startValue = Mathf.Lerp(-35, -36, t);
            endValue = Mathf.Lerp(-10, -22, t);
        }
        //  Debug.Log("Angle :"+angle);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            canChangeBool = false;
        }
        ///////  \\\\\\
        //elapsedTime += Time.deltaTime;
        //if (elapsedTime<duration)
        //{

        //    float t = elapsedTime/duration;
        //    float range = Mathf.Abs(endValue-startValue);
        //    float currentValue = Mathf.Lerp(startValue, endValue, t*range);
        //    Debug.Log("currentValue:"+currentValue);
        //    Mat[0].SetFloat(cutoffHeightPropertyName, currentValue);
        //    Mat[1].SetFloat(cutoffHeightPropertyName, currentValue);
        //    Mat[2].SetFloat(cutoffHeightPropertyName, currentValue);
        //}







        //if (!canChangeBool)
        //{
        //    Mat[0].SetFloat(cutoffHeightPropertyName, startValue);
        //    Mat[1].SetFloat(cutoffHeightPropertyName, startValue);
        //    Mat[2].SetFloat(cutoffHeightPropertyName, startValue);
        //    Debug.Log("STARTvALUE :"+startValue);
        //    Debug.Log("EndValue :"+endValue);
        //    canChangeBool = true;
        //}

        //if (canChangeBool)
        //{
        //    float currentCutoffHeight = Mat[0].GetFloat(cutoffHeightPropertyName);
        //    float newCutoffHeight = currentCutoffHeight + increaseSpeed * Time.deltaTime;
        //    Mat[0].SetFloat(cutoffHeightPropertyName, newCutoffHeight);
        //    Mat[1].SetFloat(cutoffHeightPropertyName, newCutoffHeight);
        //    Mat[2].SetFloat(cutoffHeightPropertyName, newCutoffHeight);

        //}

        if (!isChanging && isEnterField)
        {

            if (isStartEnter)
            {
                Mat[0].SetFloat(cutoffHeightPropertyName, startValue);
                Mat[1].SetFloat(cutoffHeightPropertyName, startValue);
                Mat[2].SetFloat(cutoffHeightPropertyName, startValue);
                gameObject.GetComponent<Renderer>().enabled = true;
                isStartEnter = false;
            }
            gameObject.GetComponent<Renderer>().enabled = true;
            isChanging = true;

        }
        //if (!isChanging && isEnterField && (currentValue<startValue || currentValue> end))
        //{
        //    Debug.Log("start");
        //    Mat[0].SetFloat(cutoffHeightPropertyName, startValue);
        //    Mat[1].SetFloat(cutoffHeightPropertyName, startValue);
        //    Mat[2].SetFloat(cutoffHeightPropertyName, startValue);
        //    gameObject.GetComponent<Renderer>().enabled = true;
        //    isChanging = true;

        //}

        //if (!isChanging && isExitField)
        //{
        //    currentValue = endValue;
        //    Debug.Log("deneme");
        //    Mat[0].SetFloat(cutoffHeightPropertyName, endValue);
        //    Mat[1].SetFloat(cutoffHeightPropertyName, endValue);
        //    Mat[2].SetFloat(cutoffHeightPropertyName, endValue);
        //    isChanging = true;
        //}


        if (isChanging)
        {
            elapsedTime += Time.deltaTime;


            currentValue = Mat[0].GetFloat(cutoffHeightPropertyName);

            if (isEnterField)
            {
                //if (isStartEnter)
                //{
                //    currentValue = startValue;
                //    gameObject.GetComponent<Renderer>().enabled = true;
                //    isStartEnter = false;
                //}

                newValue = Mathf.Lerp(currentValue, endValue, elapsedTime / duration);
            }
            if (isExitField)
            {

                newValue = Mathf.Lerp(currentValue, startValue, elapsedTime / duration);
                if (newValue == startValue)
                {

                    gameObject.GetComponent<Renderer>().enabled = false;
                }
            }
            Mat[0].SetFloat(cutoffHeightPropertyName, newValue);
            Mat[1].SetFloat(cutoffHeightPropertyName, newValue);
            Mat[2].SetFloat(cutoffHeightPropertyName, newValue);




            //if (isEnterField && isExitingAnimationComplete)
            //{
            //    elapsedTime += Time.deltaTime;
            //    newValue = Mathf.Lerp(currentValue, endValue, elapsedTime / duration);

            //    if (elapsedTime >= duration)
            //    {
            //        isEnteringAnimationComplete = true;
            //        elapsedTime = 0;


            //        isEnterField = false;
            //        isExitField = false;
            //        isChanging = false;
            //        canDissolve= false;
            //    }
            //    else
            //    {
            //        isEnteringAnimationComplete = false;
            //    }
            //}

            //if (isExitField && isEnteringAnimationComplete)
            //{
            //    elapsedTime += Time.deltaTime;
            //    newValue = Mathf.Lerp(currentValue, startValue, elapsedTime / duration);

            //    if (elapsedTime >= duration)
            //    {
            //        isExitingAnimationComplete = true;
            //        elapsedTime = 0;
            //        isEnterField = false;
            //        isExitField = false;
            //        isChanging = false;
            //        canDissolve= false;
            //    }
            //    else
            //    {
            //        isExitingAnimationComplete = false;
            //    }
            //}

            //Mat[0].SetFloat(cutoffHeightPropertyName, newValue);
            //Mat[1].SetFloat(cutoffHeightPropertyName, newValue);
            //Mat[2].SetFloat(cutoffHeightPropertyName, newValue);




            if (elapsedTime >= duration)
            {
                canDissolve = false;
                isChanging = false;
                isEnterField = false;
                isExitField = false;
                //if(isEnterField)
                //{
                //    Mat[0].SetFloat(cutoffHeightPropertyName, endValue);
                //    Mat[1].SetFloat(cutoffHeightPropertyName, endValue);
                //    Mat[2].SetFloat(cutoffHeightPropertyName, endValue);
                //}
                //if(isExitField)
                //{
                //    Mat[0].SetFloat(cutoffHeightPropertyName, startValue);
                //    Mat[1].SetFloat(cutoffHeightPropertyName, startValue);
                //    Mat[2].SetFloat(cutoffHeightPropertyName, startValue);
                //} 

            }
        }
        GetComponent<Renderer>().materials = Mat;















        //if (dotProductX>=0 && dotProductX<=1 && dotProductZ>=-1 && dotProductZ<=0)
        //{
        //    Debug.Log("0Setted 1.");
        //    if (!canChange1)
        //    {
        //        Mat[0].SetFloat(cutoffHeightPropertyName, -33);
        //        Mat[1].SetFloat(cutoffHeightPropertyName, -33);
        //        Mat[2].SetFloat(cutoffHeightPropertyName, -33);
        //        Debug.Log("Setted - 33");
        //        canChange1 = true;
        //    }


        //    if (canChange1)
        //    {
        //        float currentCutoffHeight = Mat[0].GetFloat(cutoffHeightPropertyName);
        //        float newCutoffHeight = currentCutoffHeight + increaseSpeed * Time.deltaTime;
        //        Mat[0].SetFloat(cutoffHeightPropertyName, newCutoffHeight);
        //        Mat[1].SetFloat(cutoffHeightPropertyName, newCutoffHeight);
        //        Mat[2].SetFloat(cutoffHeightPropertyName, newCutoffHeight);

        //        canChange2 = false;
        //        canChange3 = false;
        //        canChange4 = false;
        //    }
        //}
        //else if (dotProductX >= 0 && dotProductX <= 1 && dotProductZ >= 0 && dotProductZ <= 1)
        //{
        //    Debug.Log("0Setted 2.");
        //    if (!canChange2)
        //    {
        //        Debug.Log("Setted 2.");
        //        Mat[0].SetFloat(cutoffHeightPropertyName, -33);
        //        Mat[1].SetFloat(cutoffHeightPropertyName, -33);
        //        Mat[2].SetFloat(cutoffHeightPropertyName, -33);
        //        canChange2 = true;
        //    }

        //    if (canChange2)
        //    {
        //        float currentCutoffHeight = Mat[0].GetFloat(cutoffHeightPropertyName);
        //        float newCutoffHeight = currentCutoffHeight + increaseSpeed * Time.deltaTime;
        //        Mat[0].SetFloat(cutoffHeightPropertyName, newCutoffHeight);
        //        Mat[1].SetFloat(cutoffHeightPropertyName, newCutoffHeight);
        //        Mat[2].SetFloat(cutoffHeightPropertyName, newCutoffHeight);

        //        canChange1 = false;
        //        canChange3 = false;
        //        canChange4 = false;
        //    }
        //}
        //else if (dotProductX >= -1 && dotProductX <= 0 && dotProductZ >= 0 && dotProductZ <= 1)
        //{
        //    Debug.Log("0Setted 3.");
        //    if (!canChange3)
        //    {
        //        Debug.Log("Setted 3.");
        //        Mat[0].SetFloat(cutoffHeightPropertyName, -12);
        //        Mat[1].SetFloat(cutoffHeightPropertyName, -12);
        //        Mat[2].SetFloat(cutoffHeightPropertyName, -12);
        //        canChange3 = true;
        //    }


        //    if (canChange3)
        //    {
        //        float currentCutoffHeight = Mat[0].GetFloat(cutoffHeightPropertyName);
        //        float newCutoffHeight = currentCutoffHeight + increaseSpeed * Time.deltaTime;
        //        Mat[0].SetFloat(cutoffHeightPropertyName, newCutoffHeight);
        //        Mat[1].SetFloat(cutoffHeightPropertyName, newCutoffHeight);
        //        Mat[2].SetFloat(cutoffHeightPropertyName, newCutoffHeight);

        //        canChange1 = false;
        //        canChange2 = false;
        //        canChange4 = false;
        //    }

        //}
        //else if (dotProductX >= -1 && dotProductX <= 0 && dotProductZ >= -1 && dotProductZ <= 0)
        //{
        //    Debug.Log("0Setted 4.");
        //    if (!canChange4)
        //    {
        //        Mat[0].SetFloat(cutoffHeightPropertyName, -20);
        //        Mat[1].SetFloat(cutoffHeightPropertyName, -20);
        //        Mat[2].SetFloat(cutoffHeightPropertyName, -20);
        //        canChange4 = true;
        //    }


        //    if (canChange4)
        //    {
        //        float currentCutoffHeight = Mat[0].GetFloat(cutoffHeightPropertyName);
        //        float newCutoffHeight = currentCutoffHeight + increaseSpeed * Time.deltaTime;
        //        Mat[0].SetFloat(cutoffHeightPropertyName, newCutoffHeight);
        //        Mat[1].SetFloat(cutoffHeightPropertyName, newCutoffHeight);
        //        Mat[2].SetFloat(cutoffHeightPropertyName, newCutoffHeight);

        //        canChange1 = false;
        //        canChange2 = false;
        //        canChange3 = false;
        //    }
        //}

        //   Mat[0].SetFloat(cutoffHeightPropertyName, newCutoffHeight);

        //   GetComponent<Renderer>().materials= Mat;
    }



}
