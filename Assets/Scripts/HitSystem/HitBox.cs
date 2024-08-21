using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitBox : MonoBehaviour
{
    public Faction[] factions;
    public HitEvent hitEvent;
    
    List<HurtBox> affectedHurtBoxes = new();
    
    void OnTriggerEnter2D(Collider2D other) => HandleCollision(other);
    
    void OnTriggerStay2D(Collider2D other) => HandleCollision(other); // This happens on every frame while objects overlap, not cool I guess

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent(out HurtBox hurtBox))
        {
            return;
        }
        
        affectedHurtBoxes.Remove(hurtBox);
    }

    void HandleCollision(Collider2D other)
    {
        // * Hey J, this is a shorthand for:
        //
        // var hurtBox = other.GetComponent<HurtBox>();
        // if (hurtBox == null)
        // {
        //     return;
        // }
        // Similar to a `guard let` but in C#
        if (!other.TryGetComponent(out HurtBox hurtBox))
        {
            return;
        }
        
        if (!IsHurtBoxAvailableForHit(hurtBox))
        {
            return;
        }
        
        if (IsFactionIgnored(factions, hurtBox.factions))
        {
            return;
        }
        
        Hit(hurtBox);
    }
    
    bool IsHurtBoxAvailableForHit(HurtBox hurtBox)
    {
        return !affectedHurtBoxes.Contains(hurtBox);
    }
    
    bool IsFactionIgnored(Faction[] hitBoxFactions, Faction[] hurtBoxFactions)
    {
        if (hitBoxFactions.Length == 0 || hurtBoxFactions.Length == 0)
            return false;
        
        // TODO: make this better, probably using bitmasks
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
    
    public void Hit(HurtBox hurtBox)
    {
        if (affectedHurtBoxes.Contains(hurtBox))
        {
            return;
        }
        affectedHurtBoxes.Add(hurtBox);
        
        var hit = new Hit(hurtBox, this);
        
        hitEvent.Raise(hit);
        hurtBox.GetHit(hit);
    }
}