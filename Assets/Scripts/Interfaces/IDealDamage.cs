using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDealDamage<T>
{
    void DealDamageTank(T collision);

    void DealDamageShield(T collision);
}
