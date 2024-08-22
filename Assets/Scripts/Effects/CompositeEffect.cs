using UnityEngine;

[CreateAssetMenu(fileName = "NewCompositeEffect", menuName = "Mercadola/Effects/New Composite Effect")]
public class CompositeEffect : Effect
{
    public Effect[] effects;
    
    public override void Apply(GameObject target, GameObject? source = null)
    {
        foreach (var effect in effects)
        {
            effect.Apply(target, source);
        }
    }
}