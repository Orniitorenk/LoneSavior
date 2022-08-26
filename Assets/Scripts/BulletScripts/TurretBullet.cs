using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour, IDealDamage<Collision>
{
    [Header("Bullet Variables")]
    public BulletScript bulletScript;
    public float bulletLife = 5f;
    private GameObject target;

    private void Update()
    {
        transform.Translate(-Vector3.up * Time.deltaTime * bulletScript.speed);

        bulletLife -= Time.deltaTime;

        if(bulletLife <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(!GameManager.Instance.isGameOver)
            {
                DealDamageTank(collision);
            }
            
        }
        else if (collision.gameObject.CompareTag("Shield"))
        {
            DealDamageShield(collision);          
        }
        else
        {
            DealDamageToNone(collision);
        }

        Destroy(this.gameObject);


    }

    public void DealDamageTank(Collision collision)
    {
        target = collision.gameObject;
        target.GetComponent<TankHealth>().TakeDamage(bulletScript.bulletDamage);
        ContactPoint contact = collision.GetContact(0);
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, contact.normal);
        Vector3 position = contact.point;
        Instantiate(bulletScript.hitEffect, position, rotation);
    }
    
    public void DealDamageShield(Collision collision)
    {
        target = collision.gameObject;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Abilities>().shieldHealth -= bulletScript.bulletDamage;
        ContactPoint contact = collision.GetContact(0);
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, contact.normal);
        Vector3 position = contact.point;
        Instantiate(bulletScript.hitEffect, position, rotation);
    }

    public void DealDamageToNone(Collision collision)
    {
        target = collision.gameObject;
        ContactPoint contact = collision.GetContact(0);
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, contact.normal);
        Vector3 position = contact.point;
        Instantiate(bulletScript.hitEffect, position, rotation);
    }
}
