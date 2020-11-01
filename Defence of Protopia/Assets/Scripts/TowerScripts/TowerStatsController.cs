using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStatsController : MonoBehaviour
{
    #region Serialized Private Variables

    [SerializeField] private TowerData _turretInitialStats;

    #endregion

    #region Private Variables

    [SerializeField] private float _currentTowerDamage;

    public float CurrentTowerDamage
    {
        get
        {
            return _currentTowerDamage;
        }
    }

    [SerializeField] private float _currentTowerAttackSpeed;

    public float CurrentTowerAttackSpeed
    {
        get
        {
            return _currentTowerAttackSpeed;
        }
    }

    [SerializeField] private int _nextUpgradePrice;

    [SerializeField] private int _currentTowerLevel;

    private GameManager _gameManagerInstance;

    private SpriteRenderer TowerImage;

    private TowerUIController uiController;

    #endregion

    #region MonoBehaviour Callbacks
    
    private void Start()
    {
        _currentTowerDamage = _turretInitialStats.towerDamage;
        _currentTowerAttackSpeed = _turretInitialStats.towerAttackSpeed;
        _gameManagerInstance = FindObjectOfType<GameManager>();
        _currentTowerLevel = 1;
        _nextUpgradePrice = _turretInitialStats.initialUpgradePrice;

        //setting the reference of sprite renderer.
        TowerImage = GetComponent<SpriteRenderer>();

        //reference of UI controller
        uiController = GetComponent<TowerUIController>();
        uiController.UpgradeUIStats(_currentTowerLevel, _currentTowerDamage, _currentTowerAttackSpeed,false);
    }

    /// <summary>
    /// If we havent reached the max level, and we have enough amount of coin to upgrade
    /// 1)We increase the upgrade price
    /// 2) we increase the attack speed
    /// 3)increase the damage
    /// 4)increase the current level
    /// 5)And decrease coin amount
    /// </summary>
    public void onUpgrade()
    {
        if (_currentTowerLevel < _turretInitialStats.maxLevel && GameManager.Instance.CurrentCoinAmount >= _nextUpgradePrice)
        {

            _currentTowerDamage += _turretInitialStats.damageUpgradeValue;
            Debug.Log(CurrentTowerAttackSpeed);
            _currentTowerAttackSpeed -= _turretInitialStats.attackSpeedUpgradeValue;

            //Change the sprite of tower before upgrading as the indexing principles.
            TowerImage.sprite = _turretInitialStats.UpgradeSprites[_currentTowerLevel - 1];

            _currentTowerLevel += 1;

            //We apply _current_towerLevel+1 as if we reached max level we cant show the stats on UI.
            uiController.UpgradeUIStats(_currentTowerLevel, _currentTowerDamage, _currentTowerAttackSpeed, _currentTowerLevel == _turretInitialStats.maxLevel);

            
            //Before increasing the _next upgrade price we need to subtract it from Coin amount.
            _gameManagerInstance.DecreaseCoinAmount(_nextUpgradePrice);
            
            _nextUpgradePrice += _turretInitialStats.upgradePriceIncreaseAmount;
            //The codes for disabling upgrade button.

            
        }
    }

    


    #endregion
}
