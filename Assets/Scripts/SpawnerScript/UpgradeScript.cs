using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{

    //Tank Upgrades when you reach certain point
    [Header("Upgrades")]
    public TankShot tankShot;
    public TankHealth tankHealth;
    public PlayerMovement playerMovement;
    public Abilities shieldUpgraded;
    public Abilities upgradedCoolDownRate;
    public TankShot upgradedTimeBetweenAttack;
    public Abilities abilities;
    public GameObject tankShield;
    public bool healthUpgrade = false;

    //Shows upgrades
    [Header("UI Elements")]
    public GameObject upgradePanel;

    private void Awake()
    {
        tankShot.bulletScript.bulletDamage = 25;
    }

    public void IncreaseDamage() // Increase bullet damage method for button listener
    {
        Debug.Log("Damage increased");
        tankShot.bulletScript.UpgradeDamage(50);
        TimeScaleBackToNormal();
    }

    public void IncreaseHealth() // Increase tank health method for button listener
    {
        Debug.Log("Increase health");
        tankHealth.tankMaxHealth = 250;
        tankHealth.tankHealth = 250;
        healthUpgrade = true;
        TimeScaleBackToNormal();
    }

    public void IncreaseSpeed() // Increase tank speed method for button listener
    {
        Debug.Log("Increase Speed");
        playerMovement.speed += 1;
        TimeScaleBackToNormal();
    }

    public void IncreaseArmorSize() // Increase Armor Size method for button listener
    {
        Debug.Log("Increase Armor Size");
        abilities.shieldUpgraded = true;
        Destroy(tankShield);
        TimeScaleBackToNormal();
    }

    public void DecreaseArmorCooldown() // Decrease armor cooldown method for button listener
    {
        Debug.Log("Decrease Armor CD");
        abilities.upgradedCoolDown = true;
        TimeScaleBackToNormal();
    }

    public void DecreaseReloadSpeed() // Change fire rate of bullets method for button listener
    {
        Debug.Log("Decrease reload speed");
        tankShot.timeBetweenAttacks = 0.5f;
        TimeScaleBackToNormal();
    }

    public void IncreaseLandmineSize() // Increase landmine size method for button listener
    {
        Debug.Log("Increase Landmine size");
        abilities.landmineSizeUpgrade = true;
        TimeScaleBackToNormal();
    }
    
    public void IncreaseLandmineDamage() // Increase landmine damage method for button listener
    {
        Debug.Log("Landmine damage increased");
        abilities.landMineDamageUpgrade = true;
        TimeScaleBackToNormal();
    }
    public void TimeScaleBackToNormal()
    {
        Time.timeScale = 1;
    }

}
