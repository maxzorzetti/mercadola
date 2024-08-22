using UnityEngine;

[CreateAssetMenu(fileName = "NewRandomColorEffect", menuName = "Mercadola/Effects/New Random Color Effect")]
public class RandomColorEffect : Effect
{
    public Color[] colors = new []{ Color.black, Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.white, Color.yellow };
    
    void OnValidate()
    {
        if (colors.Length <= 1)
        {
            Debug.LogWarning("Warning!! RandomColorEffect will crash if there are less than 2 colors.");
        }
    }
    
    public override void Apply(GameObject target, GameObject? source = null)
    {
        var spriteRenderer = target.GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Color color;
            do
            {
                color = colors[Random.Range(0, colors.Length)];
            } while (color == spriteRenderer.color);
            
            spriteRenderer.color = color;
        }
    }
}