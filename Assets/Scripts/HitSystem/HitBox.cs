using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitBox : MonoBehaviour
{
    public Faction[] factions;
    public HitEvent hitEvent;
    
    public void Hit(HurtBox hurtBox)
    {
        var hit = new Hit(hurtBox, this);
        
        hitEvent.Raise(hit);
        hurtBox.GetHit(hit);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out HurtBox hurtBox)) // *
        {
            return;
        }
        
        if (IsIgnored(factions, hurtBox.factions))
        {
            return;
        }
        
        Hit(hurtBox);
        
        // * This is a shorthand for:
        //
        // var hurtBox = other.GetComponent<HurtBox>();
        // if (hurtBox == null)
        // {
        //     return;
        // }
    }
    
    bool IsIgnored(Faction[] hitBoxFactions, Faction[] hurtBoxFactions)
    {
        foreach (var a in hitBoxFactions)
        {
            foreach (var b in hurtBoxFactions)
            {
                if (a != b)
                    return false;
            }
        }

        return true;
    }
}