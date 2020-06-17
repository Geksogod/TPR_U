using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static bool CanSetDamage(GameObject damageTarget)
    {
        return damageTarget.GetComponent<IDamageable>() != null;
    }

    public static void SetDamage(GameObject damageTarget,float damageValue)
    {
        if (!CanSetDamage(damageTarget))
            throw new PersonException("Invalid damage target");
        damageTarget.GetComponent<IDamageable>().TakeDamage(damageValue);
    }
}


public interface IDamageable
{
    void TakeDamage(float damageValue);
}
