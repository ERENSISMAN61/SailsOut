using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SphereOfView : MonoBehaviour
{
    private float radius = 1000f;
    private float distance = 80f; // g�r�� alan�n�n objenin ne kadar �n�nde olaca��
    private LayerMask enemyLayer;

    private Material[] Mat;
    private float originalOpacity;

    private string cutoffHeightPropertyName = "_CutOff_Height";
    private float increaseSpeed = 4f;
    //  private bool canChange0, canChange1, canChange2, canChange3, canChange4 = false;

    private HashSet<Collider> objectsInSphere = new HashSet<Collider>();
    void Start()
    {
        enemyLayer = LayerMask.GetMask("OutlineFalse", "OutlineTrue");

    }
    void Update()
    {
        //transform.forward * distance = �n�n� daha fazla g�rmesi i�in

        Collider[] enemies = Physics.OverlapSphere(transform.position + transform.forward * distance, radius, enemyLayer); // g�r�� alan�ndaki t�m player nesnelerini bir diziye atay�n
        Debug.Log("enemy sayisi:" + objectsInSphere.Count);
        HashSet<Collider> newObjectsInSphere = new HashSet<Collider>();

        //     Debug.Log("enemy say�s�-1:"+enemies.Length);
        foreach (Collider enemy in enemies) // dizi i�inde d�ng� ba�lat
        {
            if (enemy.CompareTag("EnemyParts"))
            {

                newObjectsInSphere.Add(enemy);

                if (!objectsInSphere.Contains(enemy))
                {
                    DissolveAnimate dissolveAnimate = enemy.GetComponent<DissolveAnimate>();
                    if (dissolveAnimate != null)
                    {
                        dissolveAnimate.EnterOverlapSphere();
                    }
                }

                //CheckDistance(enemy);
                //AllowForShader(enemy);

                //Debug.Log(enemies);
                //enemy.GetComponent<Renderer>().enabled = true; // her bir player nesnesinin renderer'�n� kapat�n
                //CalculateDotProduct(enemy);
            }
        }


        foreach (Collider oldObject in objectsInSphere)
        {
            if (!newObjectsInSphere.Contains(oldObject))
            {
                DissolveAnimate dissolveAnimate = oldObject.GetComponent<DissolveAnimate>();
                if (dissolveAnimate != null)
                {
                    dissolveAnimate.ExitOverlapSphere();
                }
            }
        }

        objectsInSphere = newObjectsInSphere;

    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * distance, radius);
    }

    public HashSet<Collider> GetEnemies()
    {
        return objectsInSphere;
    }

    //void CalculateDotProductRelativeToSphere(Collider enemy)
    //{
    //    Vector3 directionFromSphereCenterToEnemy = enemy.transform.position - (transform.position + transform.forward * distance);
    //    Vector3 normalizedDirectionFromSphereCenterToEnemy = directionFromSphereCenterToEnemy.normalized;
    //    float dotProduct = Vector3.Dot(transform.forward, normalizedDirectionFromSphereCenterToEnemy);
    //    Debug.Log(dotProduct);
    //    // �ste�e ba�l�: dotProduct de�erini kullanarak istedi�iniz i�lemleri burada ger�ekle�tirin
    //}

    //void AllowForShader(Collider enemy)
    //{
    //    enemy.GetComponent<DissolveAnimate>().canDissolve = true;
    //}

    //void CalculateDotProduct(Collider enemy)
    //{


    //}



    //void CheckDistance(Collider enemy)
    //{
    //    Mat = enemy.GetComponent<Renderer>().materials;
    //    originalOpacity= 1f;
    //    float distance = Vector3.Distance(transform.position, enemy.transform.position); // Enemy ve player aras�ndaki mesafeyi hesaplay�n
    //  //  Debug.Log(distance);
    //    if (distance < 500)
    //    {
    //        //Color currentColor = new Color(1,1,1,1);
    //        //Color smoothColor =
    //        //    new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, 0, 0.5f * Time.deltaTime));
    //        ////Mat[0].color = smoothColor;
    //        ////Mat[1].color = smoothColor;
    //        ////Mat[2].color = smoothColor;

    //        //Mat[0].color = new Color(1, 1, 1, smoothColor.a);
    //        //Mat[1].color = new Color(1, 1, 1, smoothColor.a);
    //        //Mat[2].color = new Color(1, 1, 1, smoothColor.a);
    //        //  enemy.GetComponent<Renderer>().enabled = true; // Renderer'� a��n
    //    }
    //    else
    //    {
    //        //Mat[0].color = new Color(1, 1, 1, 0);
    //        //Mat[1].color = new Color(1, 1, 1, 0);
    //        //Mat[2].color = new Color(1, 1, 1, 0);
    //        //Color currentColor = Mat[0].color;
    //        //Color smoothColor =
    //        //    new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, originalOpacity, 0.5f * Time.deltaTime));
    //        //Mat[0].color = smoothColor;
    //        //Mat[1].color = smoothColor;
    //        //Mat[2].color = smoothColor;
    //        //        enemy.GetComponent<Renderer>().enabled = false; // Renderer'� kapat�n
    //    }
    //}



}
