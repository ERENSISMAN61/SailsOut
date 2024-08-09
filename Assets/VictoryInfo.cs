using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryInfo : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI coinText;
    [SerializeField]
    private TextMeshProUGUI supplyText;
    [SerializeField]
    private TextMeshProUGUI cannonBallText;

    private DestroylessManager destroylessManager;

    

    private bool _isBlueWon;
    private bool _isRedWon;
    private int enemyCount;
    private void Start()
    {
        destroylessManager = UnityEngine.Object.FindAnyObjectByType<DestroylessManager>();
        
        _isBlueWon = GameObject.FindGameObjectWithTag("GeneralCrewHealth").GetComponent<GeneralCrewHealthControl>().isBlueWon;
        _isRedWon = GameObject.FindGameObjectWithTag("GeneralCrewHealth").GetComponent<GeneralCrewHealthControl>().isRedWon;
        enemyCount = GameObject.FindGameObjectWithTag("GeneralCrewHealth").GetComponent<GeneralCrewHealthControl>().enemyCrewHealth.Count;
        Debug.Log("Enemy Count: " + enemyCount);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Sahne yükleme
    }

    private int randomValueCreate(int min, int max)
    {
        int randomValue = UnityEngine.Random.Range(min, max);

        return randomValue;
    }


    private void VictoryInfoGenerate()
    {
        if (_isBlueWon == true)
        {
            
            int RandomValue = randomValueCreate(5, 15) * enemyCount;
            int RandomValue2 = randomValueCreate(5, 15) * enemyCount;
            int RandomValue3 = randomValueCreate(5, 15) * enemyCount;
            coinText.text = destroylessManager.playerCoinDM + " + " + RandomValue.ToString();
            supplyText.text = destroylessManager.playerSupplyDM + " + " + RandomValue2.ToString();
            cannonBallText.text = destroylessManager.playerBulletDM + " + " + RandomValue3.ToString();

        }
        if (_isRedWon == true)
        {
            
            coinText.text = destroylessManager.playerCoinDM.ToString() + " + 0";
            supplyText.text = destroylessManager.playerSupplyDM.ToString() + " + 0";
            cannonBallText.text = destroylessManager.playerBulletDM.ToString() + " + 0";
        }
    }

    // Update is called once per frame
    void Update()
    {
        VictoryInfoGenerate();
    }
}
