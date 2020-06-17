using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Creature : MonoBehaviour //, IDamageable
{
    [Header("Crature value")]
    [SerializeField]
    private float health;
    [SerializeField]
    private float mana;
    [Header("Other value")]
    [SerializeField]
    private CreatureState creatureState;
    [SerializeField]
    private float damage;
    private bool dead;

    public enum CreatureState
    {
        Idle,
        Moving,
        Working,
        Attack
    }
    private Animator animator;

    public void AddHealth(float healtValue)
    {
        health += healtValue;
        if (health <= 0)
        {
            dead = true;
        }
    }

    public void SetDamage(float damageValue)
    {
        damage = damageValue;
    }

    public void AddMana(float manaValue)
    {
        mana += manaValue;
    }
    
    public bool IsDead()
    {
        return dead;
    }

    public float GetDamage()
    {
        return damage;
    }

    public bool CanDamage()
    {
        return damage > 0;
    }

    public void ChangeState(CreatureState newCreatureState)
    {
        if (creatureState != newCreatureState && !dead)
        {
            creatureState = newCreatureState;
            if (animator == null)
                animator = GetComponent<Animator>();
            animator.SetTrigger(newCreatureState.ToString());
        }
    }

    public CreatureState GetCreatureState()
    {
        return creatureState;
    }

}
