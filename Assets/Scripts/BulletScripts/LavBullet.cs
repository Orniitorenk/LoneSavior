using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavBullet : MonoBehaviour, IDealDamage<Collision>
{
    [Header("Bullet Scripts")]
    public BulletScript bulletScript;
    public float bulletLife = 5f;

    private GameObject _target;
    private Rigidbody _bulletRigidbody;

    void Awake()
    {
        _bulletRigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        _bulletRigidbody.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        transform.Translate(-Vector3.up * Time.deltaTime * bulletScript.speed);

        bulletLife -= Time.deltaTime;

        if (bulletLife <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (!GameManager.Instance.isGameOver)
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

    public void DealDamageTank(Collision collision) //interface methods
    {
        _target = collision.gameObject;
        _target.GetComponent<TankHealth>().TakeDamage(bulletScript.bulletDamage);
        ContactPoint contact = collision.GetContact(0);
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, contact.normal);
        Vector3 position = contact.point;
        Instantiate(bulletScript.hitEffect, position, rotation);
    }

    public void DealDamageShield(Collision collision)   //interface methods
    {
        _target = collision.gameObject;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Abilities>().shieldHealth -= bulletScript.bulletDamage;
        ContactPoint contact = collision.GetContact(0);
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, contact.normal);
        Vector3 position = contact.point;
        Instantiate(bulletScript.hitEffect, position, rotation);
    }

    public void DealDamageToNone(Collision collision) //interface methods
    {
        _target = collision.gameObject;
        ContactPoint contact = collision.GetContact(0);
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, contact.normal);
        Vector3 position = contact.point;
        Instantiate(bulletScript.hitEffect, position, rotation);
    }
}
