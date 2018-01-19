
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
    public int state;
    public void TakeDamage(float damage, bool crit)
    {
        if (health.CurrentVal > 0)
        {
            damage -= armor.CurrentVal;
            damage = Mathf.Clamp(damage, 0, int.MaxValue);
            health.CurrentVal -= damage;
            // FloatingTextController.Instance.CreateText(transform.position, damage.ToString(), Color.red, crit);

            if (health.CurrentVal <= 0)
            {
                Die();
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

}
