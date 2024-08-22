using UnityEngine;

[CreateAssetMenu(fileName = "NewKnockbackEffect", menuName = "Mercadola/Effects/New Knockback Effect")]
public class KnockbackEffect : Effect
{
    [Range(0, 10)]
    public float distance;
    
    public override void Apply(GameObject target, GameObject? source = null)
    {
        var direction = GetDirection(target, source);
        
        // TODO: push instead of teleporting 
        target.transform.Translate(distance * direction);
    }
    
    Vector3 GetDirection(GameObject target, GameObject? source)
    {
        if (source != null)
        {
            return (target.transform.position - source.transform.position).normalized;
        }

        if (target.TryGetComponent(out SpriteRenderer sprite))
        {
            // If the object is not rotated, return the sprite's flipX
            if (target.transform.rotation == Quaternion.identity)
            {
                return sprite.flipX ? Vector3.left : Vector3.right;
            }

            // Rotate a forward vector to the object's rotation
            // and then inverse it to get the knockback direction
            return (target.transform.rotation * Vector3.forward) * -1;
        }

        return Vector3.zero;
    }
}