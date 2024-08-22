using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageEffect", menuName = "Mercadola/Effects/New Damage Effect")]
public class DamageEffect : Effect
{
    public int damage = 1;

    public override void Apply(GameObject target, GameObject? source = null)
    {
        if (target.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
        }
    }
}