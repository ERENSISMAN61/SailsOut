using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetShipsInıtalPoints : MonoBehaviour
{

    private List<GameObject> BlueTeams = new List<GameObject>();
    private List<GameObject> RedTeams = new List<GameObject>();

    public Transform BlueTeamTransform;
    public Transform RedTeamTransform;


    // Start is called before the first frame update
    void Awake()
    {
        BlueTeams.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        RedTeams.AddRange(GameObject.FindGameObjectsWithTag("EnemyShip"));

        foreach (var blueTeam in BlueTeams)
        {
            blueTeam.transform.position = BlueTeamTransform.position;
        }

        foreach (var redTeam in RedTeams)
        {
            redTeam.transform.position = RedTeamTransform.position;
        }
    }

    
}
