using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{

    [Header("Variables")]
    public float coolDown; // ability cooldown
    public float upgradedCoolDownRate; // if you use this upgrade change cd
    public float activeShieldTime; // activated shield time
    public float shieldHealth;
    public float landmineCoolDown; // landmine cd timer
    public Transform landMineSpawnPoint; // landmine instantiated point
    [Header("Booleans")]
    private bool isCoolDown = false; // cd of shield
    private bool activeShield; // if shield is activated
    public bool shieldUpgraded = false; // checks if shield upgraded
    public bool upgradedCoolDown; // checks if shield cd upgraded
    public bool landmineIsCoolDown = false; // checks if landmine on cd
    public bool landmineSizeUpgrade = false; // checks if landmine size upgraded
    public bool landMineDamageUpgrade = false; // checks if landmine damage upgraded
    public KeyCode ability, ability1; // ability key code
    [Header("Ability GameObjects")]
    public TankHealth tankHealth; // reference of player health
    public GameObject shield, upgradedShield; // player's shield and upgraded shield reference
    public GameObject landMine, upgradedSizeLM, upgradedDamageLM, fullUpgraded; // instantiated landmine, size, damage and full upgrade
    [Header("UI")]
    public Image abilityImage; // UI of ability image
    public Image abilityImage1; // UI of ability image

    private void Start()
    {
        StartImage(); // reset image fill amount
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameOver) // checks if game over for using ability
        {
            ShieldAbility();
            DisableShield();
            LandMineAbility();
        }
        
    }

    public void ShieldAbility()
    {
        if (!shieldUpgraded) // checks if shield upgraded
        {
            if (Input.GetKeyDown(ability) && !isCoolDown && !activeShield) // checks if shield button is pressed and not on cooldown and not activated
            {
                activeShield = true;
                abilityImage.fillAmount = 1;
            }

            if (isCoolDown) // checks if shield is on cooldown
            {
                if (!upgradedCoolDown) // checks if u upgraded shield
                {
                    abilityImage.fillAmount -= 1 / coolDown * Time.deltaTime;
                }
                else
                {
                    abilityImage.fillAmount -= 1 / upgradedCoolDownRate * Time.deltaTime;
                }

                if (abilityImage.fillAmount <= 0)
                {
                    abilityImage.fillAmount = 0;
                    isCoolDown = false;
                    activeShieldTime = 4f;
                    shieldHealth = 300;
                }
            }

            if (activeShield) // checks if shield activated
            {

                if (shield != null && shieldHealth == 300) // checks if shield is not null and make shield hp equal to 300
                {
                    shield.gameObject.SetActive(true);
                }
                activeShieldTime -= Time.deltaTime;
                abilityImage.fillAmount = 1;

                if (activeShieldTime <= 0) // check if shield active timer is over and make shield disable
                {
                    isCoolDown = true;
                    if (shield != null)
                    {
                        shield.gameObject.SetActive(false);
                    }
                    activeShield = false;
                }
            }
        }

        else
        {
            if(Input.GetKeyDown(ability) && !isCoolDown && !activeShield)
            {
                activeShield = true;
                abilityImage.fillAmount = 1;
            }

            if (isCoolDown)
            {
                if (!upgradedCoolDown)
                {
                    abilityImage.fillAmount -= 1 / coolDown * Time.deltaTime;
                }
                else
                {
                    abilityImage.fillAmount -= 1 / upgradedCoolDownRate * Time.deltaTime;
                }

                if (abilityImage.fillAmount <= 0)
                {
                    abilityImage.fillAmount = 0;
                    isCoolDown = false;
                    activeShieldTime = 4f;
                    shieldHealth = 300;
                }
            }

            if (activeShield)
            {
                if (upgradedShield != null && shieldHealth == 300)
                {
                    upgradedShield.gameObject.SetActive(true);
                }
                activeShieldTime -= Time.deltaTime;
                abilityImage.fillAmount = 1;

                if (activeShieldTime <= 0)
                {
                    isCoolDown = true;
                    if (upgradedShield != null)
                    {
                        upgradedShield.gameObject.SetActive(false);
                    }
                    activeShield = false;
                }
            }
        }
        
    }


    private void DisableShield() // Disable shield for reactivating
    {
        if (!shieldUpgraded)
        {
            if (shieldHealth <= 0)
            {
                if (shield != null)
                {
                    shield.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (shieldHealth <= 0)
            {
                if (upgradedShield != null)
                {
                    upgradedShield.gameObject.SetActive(false);
                }
            }
        }
        
    }

    public void LandMineAbility()
    {
        if (Input.GetKeyDown(ability1) && !landmineIsCoolDown && !landmineSizeUpgrade && !landMineDamageUpgrade)
        {
            Instantiate(landMine, landMineSpawnPoint.position, Quaternion.Euler(-90f,0f,0f));
            landmineIsCoolDown = true;
            abilityImage1.fillAmount = 1;
        }
        else if(Input.GetKeyDown(ability1) && !landmineIsCoolDown && landmineSizeUpgrade)
        {
            Instantiate(upgradedSizeLM, landMineSpawnPoint.position, Quaternion.Euler(-90f, 0f, 0f));
            landmineIsCoolDown = true;
            abilityImage1.fillAmount = 1;
        }
        else if(Input.GetKeyDown(ability1) && !landmineIsCoolDown && landMineDamageUpgrade)
        {
            Instantiate(upgradedDamageLM, landMineSpawnPoint.position, Quaternion.Euler(-90f, 0f, 0f));
            landmineIsCoolDown = true;
            abilityImage1.fillAmount = 1;
        }
        else if(Input.GetKeyDown(ability1) && !landmineIsCoolDown && landMineDamageUpgrade && landmineSizeUpgrade)
        {
            Instantiate(fullUpgraded, landMineSpawnPoint.position, Quaternion.Euler(-90f, 0f, 0f));
            landmineIsCoolDown = true;
            abilityImage1.fillAmount = 1;
        }

        if (landmineIsCoolDown)
        {
            abilityImage1.fillAmount -= 1 / landmineCoolDown * Time.deltaTime;

            if(abilityImage1.fillAmount <= 0)
            {
                abilityImage.fillAmount = 0;
                landmineIsCoolDown = false;
            }
        }
    }

    public void StartImage()
    {
        abilityImage.fillAmount = 0;
        isCoolDown = false;
        abilityImage1.fillAmount = 0;
        landmineIsCoolDown = false;
    }
    
}
