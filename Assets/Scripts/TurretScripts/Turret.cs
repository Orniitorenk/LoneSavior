using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject turretHead; // turret head that fires bullet
    [SerializeField] private GameObject turretBullet; // turret bullet 
    [SerializeField] private Transform[] firingPoints; // turret bullet instantiated point
    [SerializeField] private GameObject brokenTurret, electricField; // when turret dies,instantiate a broken turret game object and electric field
    [SerializeField] private Transform brokenTurretPoint; // instantiated transform of broken turret and electric field
    [SerializeField] private TankHealth tankHealth; // check player health
    private GameObject target; // is player
    private bool targetLocked; // if targetLocked turret head always looks for target/player
    public bool turretAlive = true; // checks for turret alive
    public Image healthBar; // UI of turret

    public float fireRate; // shoot frequency
    public float currentHealth; // turret health
    public float maxHealth;
    private bool shotReady; // reset attack for turret
    int gunPointIndex; // how many barrel does turret have you can assign
    public GameObject damageText;
    public GameObject healthBarCanvas;

    private void Awake()
    {
        shotReady = true;
    }


    private void Update()
    {
        TargetLockedandShoot();
        Death();       
    }

    IEnumerator FireRate() // resetting shot method
    {
        yield return new WaitForSeconds(fireRate);
        shotReady = true;
    }


    void Shoot() // intantiate a bullet barrel of a turret
    {
        var gunPoint = firingPoints[gunPointIndex++];
        if(gunPoint != null)
        {
            Instantiate(turretBullet, gunPoint.position, gunPoint.rotation);
        }        
        gunPointIndex %= firingPoints.Length;
        shotReady = false;
        StartCoroutine(FireRate());
    }

    private void OnTriggerEnter(Collider other) // checks if player/target is in range
    {
        if(other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject;
            targetLocked = true;
        }
    }

    public void TakeDamage(float Damage) // Interface method
    {
        currentHealth -= Damage;
        healthBar.fillAmount = currentHealth / maxHealth;
        DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(Damage);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            target = null;
            targetLocked = false;
        }
    }

    private void Death()
    {
        if(currentHealth <= 0 && turretAlive)
        {
            Instantiate(brokenTurret, brokenTurretPoint.transform.position , Quaternion.Euler(-90f, 180f, 0f));
            electricField.gameObject.SetActive(true);          
            GetComponentInChildren<BoxCollider>().enabled = false;
            turretAlive = false;           
            Destroy(turretHead);
        }

        if(currentHealth <= 0)
        {
            Destroy(electricField, 5);
            healthBarCanvas.GetComponent<Canvas>().enabled = false;
        }
    }

    public void TargetLockedandShoot()
    {
        if (targetLocked)
        {
            if (turretHead != null)
            {
                turretHead.transform.LookAt(target.transform.position);
            }

            if (shotReady)
            {
                if (!GameManager.Instance.isGameOver)
                {
                    Shoot();
                    return;
                }

            }
        }
    }
}
