using UnityEngine;

[CreateAssetMenu(fileName = "NewTeleportEffect", menuName = "Mercadola/Effects/New Teleport Effect")]
public class TeleportEffect : Effect
{
    public Vector2 destination;
    
    public override void Apply(GameObject target, GameObject? source = null)
    {
        target.transform.position = destination;
    }
}