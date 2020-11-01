using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableData/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float enemySpeed;

    public int enemyDamage;

    public int enemyHealth;
}
