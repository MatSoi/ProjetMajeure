﻿
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField]
    protected Stat health;

    [SerializeField]
    protected Stat energy;

    [SerializeField]
    protected Stat shield;

    [SerializeField]
    public Stat attack_damage;
    [SerializeField]
    public Stat armor;


    [SerializeField]
    public StateType state;


    // Damage Manager
    public void TakeDamage(float damage, bool crit)
    {
        //If the character has a shield, we reduce it before health
        if (shield.CurrentVal > 0)
        {
            damage -= armor.CurrentVal;
            damage = Mathf.Clamp(damage, 0, int.MaxValue);
            if (shield.CurrentVal > damage)
                shield.CurrentVal -= damage;
            if (shield.CurrentVal < damage)
            {
                damage -= shield.CurrentVal;
                shield.CurrentVal = 0;
                health.CurrentVal -= damage;
                if (health.CurrentVal <= 0)
                {
                    Die();
                }

            }
            // FloatingTextController.Instance.CreateText(transform.position, damage.ToString(), Color.red, crit);
        }
        else
        {
            if (health.CurrentVal > 0)
            {
                damage -= armor.CurrentVal;
                damage = Mathf.Clamp(damage, 0, int.MaxValue);
                health.CurrentVal -= damage;
                //FloatingTextController.Instance.CreateText(transform.position, damage.ToString(), Color.red, crit);

                if (health.CurrentVal <= 0)
                {
                    Die();
                }
            }
        }
    }

    public void Heal(float heal, bool crit)
    {
        health.CurrentVal = Mathf.Clamp(health.CurrentVal + heal, 0, 100);
        // FloatingTextController.Instance.CreateText(transform.position, heal.ToString(), Color.green, crit);
    }
    public virtual void Die()
    {
        // FloatingTextController.Instance.CreateText(transform.position, "DIES", Color.black, true);
    }
    public void Awaken()
    {
        state = 0;
        // FloatingTextController.Instance.CreateText(transform.position, heal.ToString(), Color.green, crit);
    }
    public bool isNormal()
    {
        if(state == 0) return true;
        return false;
        // FloatingTextController.Instance.CreateText(transform.position, heal.ToString(), Color.green, crit);
    }
    public void isPoisoned()
    {
        health.CurrentVal -= 10;
        if (health.CurrentVal <= 0)
        {
            Die();
        }
        // FloatingTextController.Instance.CreateText(transform.position, heal.ToString(), Color.green, crit);
    }
}

public enum StateType
{
    NORMAL,
    SLEEP,
    POISONED,
    CONFUSED

};