using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("The storage that containt enemy data")]
    public EnemyData enemyData;

    [Header("The gameObjects that we use them as waypoints.")]
    public Transform[] enemyPath;

    [Header("The healthbar sprite that is used for enemy health")]

    [SerializeField] private SpriteRenderer HealthFillbar;


    #region Private Variables

    //The reference for GameManager instance.
    GameManager _gameManagerInstance;

    //Using this we define the initial point of enemy.
    private int _currentWayPointIndex = 0;

    //When the enemy pass any waypoint we will the switching time.
    private float _lastWayPointSwitchTime;

    //The point where enemy begins movement.
    Vector3 _startPos;

    //The point where enemy goes from startPos
    Vector3 _endPos;

    //The distance between start and end pos. We use it for time calculations with speed.
    float _distanceBetTwoPoints;

    //The time that is used for passing from start pos to end pos.
    float _timeForPath;

    //CurrentHealth of Player.(we use this var for decreasing healthbar visually)
    float _currentHealthAmount;
    
    #endregion


    private void Start()
    {
        SetInitialValues();
    }

    private void Update()
    {
        UpdatePosition();

    }

    private void UpdatePosition()
    {
        //We calculate the currentTime until currentTimeOnPath is equal to _timeForPath
        float currentTImeOnPath = Time.time - _lastWayPointSwitchTime;

        //We set currentTimeOnPath/_timeForPath in order to equally move the enemy to endposition.
        //Otherwise different float values can spoil our if statement below.s
        transform.position = Vector2.Lerp(_startPos, _endPos, currentTImeOnPath / _timeForPath);

        if (gameObject.transform.position == _endPos)
        {
            _currentWayPointIndex += 1;

            //When er reached the last waypoint of way.(Our base)
            if (_currentWayPointIndex == enemyPath.Length - 1)
            {
                //We need to damage the base.
                _gameManagerInstance.DamageBase(enemyData.enemyDamage);
                //And destroy the object.
                Destroy(this.gameObject);
                return;
            }
            //We set all values again for the current and next way point.
            _lastWayPointSwitchTime = Time.time;
            _startPos = enemyPath[_currentWayPointIndex].position;
            _endPos = enemyPath[_currentWayPointIndex + 1].position;
            _distanceBetTwoPoints = Vector3.Distance(_startPos, _endPos);
            _timeForPath = _distanceBetTwoPoints / enemyData.enemySpeed;

            //Rotate the objects face to current waypoint
            //Relative direction from enemy to way point.
            var dir = _endPos - transform.position;     

            //Degree between them.
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;      

            //Rotating enemy along x axis with this angle.
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);      
        }
    }

    private void SetInitialValues()
    {
        //The reference for GameManager.
        _gameManagerInstance = GameManager.Instance;

        //Initially we set the lastWayPointSwitch time for first waypoint.
        _lastWayPointSwitchTime = Time.time;
        _startPos = enemyPath[_currentWayPointIndex].position;
        _endPos = enemyPath[_currentWayPointIndex + 1].position;
        _distanceBetTwoPoints = Vector3.Distance(_startPos, _endPos);
        _timeForPath = _distanceBetTwoPoints / enemyData.enemySpeed;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, enemyPath[_currentWayPointIndex + 1].rotation,1);
        _currentHealthAmount = enemyData.enemyHealth;
    }

    private void DecreaseEnemyHealth(float DamageAmount)
    {
        _currentHealthAmount -= DamageAmount;
        HealthFillbar.size =new Vector2(HealthFillbar.size.x,Mathf.Lerp(0,1,_currentHealthAmount/enemyData.enemyHealth));
        if (_currentHealthAmount <= 0)
        {
            _gameManagerInstance.IncreaseCoinAmount(Random.Range(1,enemyData.MaxRewardCount));
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("bullet"))
        {
            BulletController bulletController = collision.gameObject.GetComponent<BulletController>();

            DecreaseEnemyHealth(bulletController.Damage);

            //Enemy decrease health with ammo damage.
            Destroy(collision.gameObject);
        }
    }
}
//int _currentPointIndex = 0;

//Transform next_path_point;
//private void Start()
//{
//    enemyPath = GameManager.Instance.EnemyPath;
//    next_path_point = enemyPath[_currentPointIndex +=1];
//}

//private void Update()
//{
//    WalkThroughPath();
//}



//private void WalkThroughPath()
//{

//    transform.position = Vector3.Lerp(transform.position, next_path_point.position, Time.deltaTime);
//    if (Vector3Int.RoundToInt(transform.position)==(Vector3Int.RoundToInt(next_path_point.position)))
//    {
//        next_path_point = enemyPath[_currentPointIndex += 1];
//    } 
//}//Commented test code