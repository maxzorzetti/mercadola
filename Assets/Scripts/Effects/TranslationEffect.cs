using UnityEngine;

[CreateAssetMenu(fileName = "NewTranslationEffect", menuName = "Mercadola/Effects/New Translation Effect")]
public class TranslationEffect : Effect
{
    public Vector2 translation;

    public override void Apply(GameObject target, GameObject? source = null)
    {
        target.transform.Translate(translation);
    }
}