using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelManager : MonoBehaviour
{

    [SerializeField] public float playerLevel;
    public float playerDamage;
    public float damageMultiple = 5;
    private Vector3 levelScale, levelMultiple;

    [SerializeField] public TextMeshProUGUI levelText;

    private float playerMaxHealth, playerHealth;
    
    private float healthMultiple = 40;
    private float healthLevelIncrease = 10;

    [SerializeField] private float XP;
    private void Awake()
    {
        playerDamage = playerLevel * damageMultiple + 5;
        //gameObject.transform.localScale = levelScale + levelMultiple * playerLevel;
        

    }

    private void Start()
    {
     //   levelScale = new Vector3(0.6f, 0.6f, 0.6f);
        levelMultiple = new Vector3(0.15f, 0.15f, 0.15f);


     //   HealthController(); // Destroyless �al��t��� zaman can� bu script kontrol etmesin diye kapat�ld�.
        LevelController();  // level artt���nda bu fonksiyonu yeniden �al��t�rs�n!!!!!!!!!!!!!!!  xp 100 oldu�unda xp'yi 0 yap ve bu fonksiyonu �al��t�r gibi...
    }
    private void Update()
    {
       if(XP >= 100f)
        {
            XP -= 100f;
            playerLevel++;
            HealthController();
            LevelController();

        }

        levelText.text = "lvl\n"+playerLevel.ToString();


    }
    private void LevelController()
    {
        playerDamage = playerLevel * damageMultiple + 5;
    //    gameObject.transform.localScale = levelScale + levelMultiple * playerLevel;
    }

    private void HealthController()
    {

        playerMaxHealth = playerLevel * healthMultiple + 100f;



        ///////////:::::::::::: ONEMLI   :::::::::   HEALTH ba�ka bir scripte ta��nd��� zaman          
        ////////// , GetComponent<ShipMovementScript>() burdaki script ismi de�i�ecek
        playerHealth = gameObject.GetComponentInChildren<PlayerHealthBarControl>().health + healthLevelIncrease ;
        gameObject.GetComponentInChildren<PlayerHealthBarControl>().maxHealth = playerMaxHealth;
        gameObject.GetComponentInChildren<PlayerHealthBarControl>().health = playerHealth;
    }
}
