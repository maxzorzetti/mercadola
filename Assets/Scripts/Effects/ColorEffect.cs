using UnityEngine;

[CreateAssetMenu(fileName = "NewColorEffect", menuName = "Mercadola/Effects/New Color Effect")]
public class ColorEffect : Effect
{
    public Color color;

    public override void Apply(GameObject target, GameObject? source = null)
    {
        if (target.TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            spriteRenderer.color = color;
        }
    }
}