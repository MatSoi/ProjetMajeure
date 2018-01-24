using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour {

    public void AttackSO(Character Attacker, Character Attacked)
    {
        // Critical attack
        float random = Random.Range(Attacker.attack_damage.CurrentVal, 2 * Attacker.attack_damage.CurrentVal);
        if (random >= 1.5 * Attacker.attack_damage.CurrentVal)
        {
            Attacked.TakeDamage(random, true);
        }
        else
        {
            Attacked.TakeDamage(random, false);
        }

    }
}
