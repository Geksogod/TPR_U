using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Human))]
public class MeleWarriorClass : HumanClass
{
    [SerializeField]
    private Human human;
    private float damageOfClass;



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject?.GetComponent<IDamageable>() != null && human.CanDamage())
        {

        }
    }
}
