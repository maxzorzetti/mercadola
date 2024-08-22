using UnityEngine;

[CreateAssetMenu(fileName = "NewHealEffect", menuName = "Mercadola/Effects/New Heal Effect")]
public class HealEffect : Effect
{
    [Range(0, 10)]
    public int healAmount = 1;
    
    public override void Apply(GameObject target, GameObject? source = null)
    {
        if (target.TryGetComponent(out Health health))
        {
            health.Heal(healAmount);
        }
    }
}