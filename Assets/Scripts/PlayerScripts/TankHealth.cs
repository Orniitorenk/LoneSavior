using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour, IDamageable
{
    [Header("Tank Health Variables")]
    public float tankHealth;
    public float tankMaxHealth = 200;
    public UpgradeScript upgradeScript;
    
    [Header("Animated Death and Effects Variables")]
    public GameObject tankTurretExist;
    public GameObject tankTurret;
    public GameObject flames;
    public GameObject gears;
    public Transform tankTurretPoint;
    public Image healthBar;
    public GameObject damageText;
    public float amountOfHP = 50;

    [Header("Booleans")]
    public bool takeHeal = false;
    public bool isInstantiated = false;



    public void FixedUpdate()
    {
        if(tankHealth <= 0 && !isInstantiated)
        {
            Die();
            GameManager.Instance.isGameOver = true;
        }

        if(tankHealth > 200 && !upgradeScript.healthUpgrade)
        {
            tankHealth = 200;
        }
        else if(tankHealth > 250 && upgradeScript.healthUpgrade)
        {
            tankHealth = 250;
        }

        healthBar.fillAmount = tankHealth / tankMaxHealth;
        
    }

    public void TakeDamage(float Damage)
    {
        tankHealth -= Damage;
        healthBar.fillAmount = tankHealth / tankMaxHealth;

        DamageIndicator indicator = Instantiate(damageText, transform.position + new Vector3(0f, 2f, 0), Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(Damage);
    }

    public void Die()
    {       
        GetComponent<PlayerMovement>().enabled = false;
        Destroy(tankTurretExist.gameObject);
        Instantiate(tankTurret, tankTurretPoint.position, Quaternion.identity);
        Instantiate(flames, tankTurretPoint.position , Quaternion.Euler(-90f,0f,0f));
        Instantiate(gears, tankTurretPoint.position, Quaternion.Euler(-90f, 0f, 0f));
        isInstantiated = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Health"))
        {
            if (tankHealth != 200 && !upgradeScript.healthUpgrade)
            {
                tankHealth += amountOfHP;
                Debug.Log(tankHealth);
                Destroy(other.gameObject);
            }
            else if (tankHealth != 250 && upgradeScript.healthUpgrade)
            {
                tankHealth += amountOfHP;
                Debug.Log(tankHealth);
                Destroy(other.gameObject);
            }     
        }
    }
}
