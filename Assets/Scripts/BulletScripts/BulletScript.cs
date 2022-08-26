using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullets" , menuName = ("Bullets Configuration"))]
public class BulletScript : ScriptableObject
{
    public float speed;
    public float bulletLife;
    public float bulletDamage;
    public GameObject hitEffect;
        

    public void UpgradeDamage(float damage)
    {
        bulletDamage = damage;
    }
}
