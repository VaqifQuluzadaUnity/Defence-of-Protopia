using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackController : MonoBehaviour
{
    //The list of targets that in range
    [SerializeField] List<GameObject> _targetList=new List<GameObject>();

    //the Prefab of bullet
    [SerializeField] GameObject _bulletPrefab;

    //The stats of current tower(we need only attack speed(frequency) and damage.
    TowerStatsController _towerStats;

    //That is used for not starting more than one coroutine
    public bool isAttacking=false;
    private void Start()
    {
        //Get the reference for the current tower.
        _towerStats = GetComponent<TowerStatsController>();
    }


    private void Update()
    {
        if (!isAttacking && _targetList.Count !=0)
        {
            StartCoroutine(Attack());
        }
        else
        {
            StopCoroutine(Attack());
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("enemy"))
        {
            _targetList.Add(collision.gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_targetList.Contains(collision.gameObject))  //If target exits the range
        {
            _targetList.Remove(collision.gameObject);    //we remove it from list
            _targetList.RemoveAll(gm=>gm==null);         //And remove its null place
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        for(int i = 0; i < _targetList.Count; i++)
        {
            GameObject Bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
            Bullet.GetComponent<BulletController>().Damage = _towerStats.CurrentTowerDamage;
            Bullet.GetComponent<BulletController>().Target = _targetList[i];
            yield return new WaitForSeconds(_towerStats.CurrentTowerAttackSpeed);
        }
        
        isAttacking = false;
    }
    
}
