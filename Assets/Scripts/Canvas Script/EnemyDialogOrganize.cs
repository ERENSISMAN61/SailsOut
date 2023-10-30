using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDialogOrganize : MonoBehaviour
{


    private TextMeshProUGUI conversationText;

    private GameObject enemyDialog;
    private GameObject spawnEnemyDialog;

    GameObject AttackButtonObject;
    GameObject PayButtonObject;
    GameObject SurrenderButtonObject;

    private void Start()
    {
        //      enemyDialog = (GameObject)Resources.Load("Resources/Prefabs/Canvas Prefabs/EnemyDialog");
        enemyDialog = Resources.Load<GameObject>("Prefabs/Canvas Prefabs/EnemyDialog");
        Debug.Log("eren"+enemyDialog);
    }
    private void Update()
    {
        if(gameObject.GetComponent<SmoothAgentMovement>().didCatch)
        {
            spawnEnemyDialog = Instantiate(enemyDialog, new Vector3(+960, +540, 0), Quaternion.identity, GameObject.Find("Canvas").transform);  // enemy dialogu Spawnla
                                                                                                                                                /////\\\\\
            AttackButtonObject = GameObject.Find("AttackButton");
            PayButtonObject = GameObject.Find("PayButton");
            SurrenderButtonObject = GameObject.Find("SurrenderButton");

            AttackButtonObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => gameObject.GetComponent<EnemyDialog>().AttackButton("BattleScene"));
            PayButtonObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => gameObject.GetComponent<EnemyDialog>().PayButton());
            SurrenderButtonObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => gameObject.GetComponent<EnemyDialog>().SurrenderButton());
            /////\\\\\

            conversationText = GameObject.FindGameObjectWithTag("EnemyText").GetComponent<TextMeshProUGUI>();
            conversationText.text = "I've got you cornered. Surrender or we will attack.";
        }
    }
}
