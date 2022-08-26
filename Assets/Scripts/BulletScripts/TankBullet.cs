using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    [Header("Bullet Variables")]
    public float bulletLife = 5;
    public BulletScript bulletScript;
    private Rigidbody _bulletRigidbody;
    public TankShot tankShot;

    private void Awake()
    {
        _bulletRigidbody = GetComponent<Rigidbody>();
        tankShot = GameObject.FindGameObjectWithTag("Player").GetComponent<TankShot>();
    }

    private void OnEnable()
    {
        bulletLife = 5;
        _bulletRigidbody.velocity = Vector3.zero;
    }

    public void OnDisableGameObject(GameObject bullet)
    {        
        bullet.SetActive(false);
        tankShot._bulletPool.Enqueue(bullet);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.back * Time.deltaTime * bulletScript.speed);

        bulletLife -= Time.deltaTime;

        if(bulletLife <= 0)
        {
            OnDisableGameObject(this.gameObject);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lav25Spawned"))
        {
            GiveDamage(collision);
        }
        else if (collision.gameObject.CompareTag("Turret"))
        {
            GameObject enemyPrefab = collision.transform.gameObject;
            enemyPrefab.GetComponentInParent<Turret>().TakeDamage(bulletScript.bulletDamage);
            ContactPoint contact = collision.GetContact(0);
            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, contact.normal);
            Vector3 position = contact.point;
            Instantiate(bulletScript.hitEffect, position, rotation);
        }
        else
        {
            GameObject enemyPrefab = collision.transform.gameObject;
            ContactPoint contact = collision.GetContact(0);
            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, contact.normal);
            Vector3 position = contact.point;
            Instantiate(bulletScript.hitEffect, position, rotation);
        }

        OnDisableGameObject(this.gameObject);


    }

    public void GiveDamage(Collision collision)
    {
        GameObject enemyPrefab = collision.transform.gameObject;
        enemyPrefab.GetComponent<EnemySpawningEnemies>().TakeDamage(bulletScript.bulletDamage);
        ContactPoint contact = collision.GetContact(0);
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, contact.normal);
        Vector3 position = contact.point;
        Instantiate(bulletScript.hitEffect, position, rotation);
    }
    
    




}
