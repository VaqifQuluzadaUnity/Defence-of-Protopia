using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used for holding data about different towers.
/// </summary>
[CreateAssetMenu(fileName ="TurretData" ,menuName ="ScriptableData/TurretData")]
public class TowerData : ScriptableObject
{
    public float towerDamage;
    
    public float towerAttackSpeed;

    //Initial upgrade price of tower
    public int initialUpgradePrice;

    //The increase amount of damage
    public int damageUpgradeValue;

    //The increase amount of speed
    public float attackSpeedUpgradeValue;

    //The icrease amount of upgrade price
    public int upgradePriceIncreaseAmount;

    //max level that tower can be upgraded
    public int maxLevel;

    public Sprite[] UpgradeSprites;


}
