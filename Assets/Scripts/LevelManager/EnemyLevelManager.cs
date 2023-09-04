using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int enemyLevel = 1;
    public int enemyDamage;
    public int damageMultiple = 5;
    private Vector3 levelScale,levelMultiple;

    private float scalePercent;
    
    public TextMeshProUGUI LevelText;

    private float enemyHealth;
    private float enemyMaxHealth;
    private float healthMultiple = 40;

    private void Start()
    {
        levelScale = new Vector3(1.55f, 1.55f, 1.55f);
        levelMultiple = new Vector3(0.15f, 0.15f, 0.15f);

        HealthController();
        LevelController();
    }

    private void Update()
    {

        LevelText.text = enemyLevel.ToString();

    }
    private void LevelController()
    {
        enemyDamage = enemyLevel * damageMultiple;

        scalePercent = (levelScale.x + (levelMultiple.x) * enemyLevel) /(levelScale.x + levelMultiple.x * 1);
      
        gameObject.transform.localScale = levelScale + levelMultiple * enemyLevel;

       
        //  gameObject.gameObject.GetComponent<CircleCollider2D>().transform.localScale =  gameObject.gameObject.GetComponent<CircleCollider2D>().transform.localScale / enemyLevel;

        GetComponentInChildren<CircleCollider2D>().gameObject.transform.localScale =  GetComponentInChildren<CircleCollider2D>().gameObject.transform.localScale / scalePercent;
    }

    private void HealthController()
    {
        enemyMaxHealth = enemyLevel * healthMultiple + 60f;
        enemyHealth = enemyLevel * healthMultiple + 60f;
        gameObject.GetComponent<EnemyShipsController>().maxHealth = enemyMaxHealth;
        gameObject.GetComponent<EnemyShipsController>().health = enemyHealth;
    }

}
