#nullable enable
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtBox : MonoBehaviour
{
    public Faction[] factions = null!;
    public HurtEvent hurtEvent = null!;

    public void GetHit(Hit hit)
    {
        hurtEvent.Raise(hit);
    }
}