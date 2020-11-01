using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableData/EnemyData")]
public class EnemyData : ScriptableObject
{
    //Speed of enemy
    public float enemySpeed;

    //Damage of enemy
    public int enemyDamage;

    //Health of enemy
    public int enemyHealth;

    //Max how much coin can this enemy give us
    public int MaxRewardCount;
}
