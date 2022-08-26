using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffects : MonoBehaviour
{
    public float particleEffectLife = 1;

    
    void Update()
    {
        particleEffectLife -= Time.deltaTime;

        if(particleEffectLife <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
