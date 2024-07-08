using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsContainer : MonoBehaviour
{
    public float rank;
    public float health;
    public float attackPower;


    public UnitsContainer(float rank, float health, float attackPower)
    {
        this.rank = rank;
        this.health = health;
        this.attackPower = attackPower;
    }



}
