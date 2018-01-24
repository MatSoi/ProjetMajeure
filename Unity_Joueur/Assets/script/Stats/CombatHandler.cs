using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour {

    public void AttackSO(Character Attacker, Character Attacked)
    {
        if (Attacker.state.ToString() == "SLEEP")
        {
            int random_sleep = Random.Range(0,4);
            if (random_sleep == 0)
                Attacker.Awaken();
            else
                return;
        }
        if (Attacker.state.ToString() == "CONFUSED")
        {
            int random_sleep = Random.Range(0, 5);
            //Attacker hit itself
            if (random_sleep == 4)
            {
                Attacker.TakeDamage(Attacker.attack_damage.CurrentVal, false);
                return;
            }
            //Attacker hit itself
            if (random_sleep == 0)
            {
                Attacker.Awaken();
            }

        }

        // Critical attack
        float damage_dealt = Random.Range(Attacker.attack_damage.CurrentVal, 2 * Attacker.attack_damage.CurrentVal);
        if (damage_dealt >= 1.5 * Attacker.attack_damage.CurrentVal)
        {
            Attacked.TakeDamage(damage_dealt, true);
        }
        else
        {
            Attacked.TakeDamage(damage_dealt, false);
        }

        if (Attacker.state.ToString() == "POISONED")
        {
            Attacker.isPoisoned();
        }
    }

    public void setState(Character Attacked, StateType new_state)
    {
        if (Attacked.isNormal())
        {
            Attacked.state = new_state;
        }
        else
        {
            //Attacked has already a state
        }
    }
}

