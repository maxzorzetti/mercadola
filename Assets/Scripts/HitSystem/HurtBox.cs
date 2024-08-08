using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtBox : MonoBehaviour
{
    public Faction[] factions;
    public HurtEvent hurtEvent;

    public void GetHit(Hit hit)
    {
        hurtEvent.Raise(hit);    
    }
}