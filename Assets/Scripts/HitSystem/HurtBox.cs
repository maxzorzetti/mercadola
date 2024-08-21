#nullable enable
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtBox : MonoBehaviour
{
    public Faction[] factions = null!;
    public HurtEvent hurtEvent = null!;
    
    [DoNotSerialize]
    public Health? health;

    void Start()
    {
        health = GetComponent<Health>();
    }

    public void GetHit(Hit hit)
    {
        if (health != null)
        {
            HandleHealth(hit);
        }
        
        hurtEvent.Raise(hit);
    }

    void HandleHealth(Hit hit)
    {
        health!.TakeDamage(hit.hitBox.GetComponent<Damage>()?.damage ?? Damage.DefaultDamage, hit.hitBox);
    }
}