using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Public Variables

    //The static instance of GameManager that is controlled by different scripts.s
    public static GameManager Instance;

    public Transform[] EnemyPath;

    //The list of enemy prefabs.
    public GameObject[] EnemyPrefabs;
    
    private int _numberOfKilledEnemies;

    public int countOfWaves=0;
    #endregion

    #region Private/Public Serialized Variables

    //For initial level we can set whatever value we want.
    //This is x for our initial spawning. Then we set it as the result of random calculation.
    [SerializeField] int WaveInitialEnemyCount;

    //Using this in random we set the max number of enemy count increment in a wave.
    [SerializeField] int WaveEnemyMaxIncreaseRate;

    //The time that is used for starting sequence of next wave.
    [SerializeField] int WaveTimer = 0;

    //We set it for 5 as the player waits 5 seconds and prepare for battle
    [SerializeField] int MaxWaveDelay = 60;

    //The spawn interval between two enemies.
    [SerializeField] float enemySpawnInterval=0.5f;

    //For decreasing health with lerp we use following variable.
    public int MaxBaseHealth;
    #endregion

    #region Private Get-Set variables

    //The amount of coin that player has
    public int _currentCoinAmount = 0;

    //For security purposes(anti-cheating) we define setter-getter version of private variable.
    public int CurrentCoinAmount
    {
        get
        {
            return _currentCoinAmount;
        }
    }

    #endregion


    #region Monobehaviour callbacks

    private void Awake()
    {
        GameManager.Instance = this;
        InvokeRepeating("SpawnEnemy", 0, MaxWaveDelay);
    }
    #endregion

    void SpawnEnemy()
    {
        StartCoroutine(SpawnEnemyWithDelay());
    }


    IEnumerator SpawnEnemyWithDelay()
    {
        
        for(int i = 0; i < WaveInitialEnemyCount; i++)
        {
            //We check that our count of waves doesent exceeed the number of enemy prefabs.
            countOfWaves = countOfWaves < EnemyPrefabs.Length ? countOfWaves : EnemyPrefabs.Length-1;
            int enemy_index = Random.Range(0, countOfWaves+1);
            GameObject new_enemy = Instantiate(EnemyPrefabs[enemy_index], EnemyPath[0].position, EnemyPath[0].rotation);
            EnemyController enemyController = new_enemy.GetComponent<EnemyController>();
            enemyController.enemyPath = EnemyPath;
            yield return new WaitForSeconds(enemySpawnInterval);
            
        }
        WaveInitialEnemyCount = Random.Range(WaveInitialEnemyCount, WaveInitialEnemyCount + WaveEnemyMaxIncreaseRate);
        countOfWaves += 1;
    }



    #region Public methods

    public void DecreaseCoinAmount(int decreasedValue)
    {
        _currentCoinAmount -= decreasedValue;
    }

    public void IncreaseCoinAmount(int increaseValue)
    {
        _currentCoinAmount += increaseValue;
    }

    public void IncreaseKilledEnemyCount()
    {
        _numberOfKilledEnemies += 1;
    }

    public void DamageBase(int DamageAmount)
    {
        MaxBaseHealth -= DamageAmount;
    }
    #endregion


}
