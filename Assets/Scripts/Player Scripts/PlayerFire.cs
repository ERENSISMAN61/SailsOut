using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 2f;
    public float bulletSpeedrate = 5;
    
    void Start()
    {
        
    }
    void FireBullet()
    {

        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        newBullet.GetComponent<BulletController>().playerBulletDamage = gameObject.GetComponent<PlayerLevelManager>().playerDamage;
        newBullet.GetComponent<BulletController>().SetVelocity(firePoint.right * bulletSpeed / bulletSpeedrate);

        gameObject.GetComponent<AudioGet>().PlayShotAudio();


        Destroy(newBullet, 3f);


    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) ) //////////////////     Veysel buraya bakılacak        \\\\\\\\\\\\\\\\\\\\\\
        {

            FireBullet();


        }
    }
}
