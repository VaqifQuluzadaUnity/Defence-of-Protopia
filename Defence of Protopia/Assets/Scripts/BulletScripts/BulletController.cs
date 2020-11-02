using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Faqan Aliyev
public class BulletController : MonoBehaviour
{
    public float Damage;
    public GameObject Target;
    public float BulletSpeed;


    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position,Target.transform.position,BulletSpeed);
    }
}
