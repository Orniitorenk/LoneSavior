using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyButton : MonoBehaviour
{

    public GameObject damageText,shieldText, healthText, speedText, decreaseArmorCDText, decreaseReloadText, landmineSizeText, landmineDamageText;

    public void DestroyAttackDamage()
    {
        Destroy(damageText, 2);
    }
    public void DestroyShield()
    {
        Destroy(shieldText, 2);
    }

    public void DestroyHealth()
    {
        Destroy(healthText, 2);
    }

    public void SpeedText()
    {
        Destroy(speedText, 2);
    }

    public void DecreaseArmorCD()
    {
        Destroy(decreaseArmorCDText, 2);
    }

    public void DecreaseReloadText()
    {
        Destroy(decreaseReloadText, 2);
    }
    
    public void LandmineSize()
    {
        Destroy(landmineSizeText, 2);
    }

    public void LandmineDamage()
    {
        Destroy(landmineDamageText, 2);
    }
    
    public void DeactiveAllButtons()
    {
        if(damageText != null)
        {
            damageText.SetActive(false);
        }
        if (shieldText != null)
        {
            shieldText.SetActive(false);
        }
        if (healthText != null)
        {
            healthText.SetActive(false);
        }
        if (speedText != null)
        {
            speedText.SetActive(false);
        }
        if (decreaseArmorCDText != null)
        {
            decreaseArmorCDText.SetActive(false);
        }
        if (decreaseReloadText != null)
        {
            decreaseReloadText.SetActive(false);
        }
        if(landmineSizeText != null)
        {
            landmineSizeText.SetActive(false);
        }
        if(landmineDamageText != null)
        {
            landmineDamageText.SetActive(false);
        }
    }

}
