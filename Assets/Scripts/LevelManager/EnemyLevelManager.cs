using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int enemyLevel = 0;
    public int enemyDamage;
    public int damageMultiple = 5;
    private Vector3 levelScale,levelMultiple;

    private float scalePercent;
    
    public TextMeshProUGUI LevelText;

    private float enemyHealth;
    private float enemyMaxHealth;
    private float healthMultiple = 40;

    [SerializeField] private float XP;
    private void Start()
    {
        levelScale = new Vector3(1f, 1f, 1f);
        levelMultiple = new Vector3(0.10f, 0.10f, 0.10f);

        HealthController();
        LevelController();
    }

    private void Update()
    {

        LevelText.text = enemyLevel.ToString();

        if (XP >= 100f)
        {
            XP -= 100f;
            enemyLevel++;
            HealthController();
            LevelController();

        }

    }
    private void LevelController()
    {
        enemyDamage = enemyLevel * damageMultiple + 5;

       // scalePercent = (levelScale.x + (levelMultiple.x) * enemyLevel) /(levelScale.x + levelMultiple.x * 1);
      
        gameObject.GetComponentInChildren<MeshRenderer>().transform.parent.localScale = levelScale + levelMultiple * enemyLevel;



        //  gameObject.gameObject.GetComponent<CircleCollider2D>().transform.localScale =  gameObject.gameObject.GetComponent<CircleCollider2D>().transform.localScale / enemyLevel;
        
    }

    private void HealthController()
    {
        enemyMaxHealth = enemyLevel * healthMultiple + 100f;
        enemyHealth = enemyLevel * healthMultiple + 100f;
        gameObject.GetComponent<EnemyShipsController>().maxHealth = enemyMaxHealth;
        gameObject.GetComponent<EnemyShipsController>().health = enemyHealth;
    }

}
