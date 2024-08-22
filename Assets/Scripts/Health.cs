using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Range(0, 10)]
    public int maxHealth = 3;
    [Range(0, 10)]
    public int currentHealth = 3;
    
    public bool fullHealthStart = true;

    public bool IsDead;
    [DoNotSerialize]
    public bool IsAlive => !IsDead;
    
    public UnityEvent<int> OnHeal;
    public UnityEvent<int> OnDamageTaken;
    public UnityEvent<MonoBehaviour> OnDeath;
    public UnityEvent OnRevive;
    
    public IntegerEvent OnHealEvent;
    public IntegerEvent OnDamageTakenEvent;
    public SourceEvent OnDeathEvent;
    public Event OnReviveEvent;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = fullHealthStart ? maxHealth : currentHealth;
    }

    public void Heal(int amount)
    {
        if (amount <= 0)
        {
            return;
        }
        
        var amountToHeal = Mathf.Min(maxHealth - currentHealth);
        
        currentHealth += amountToHeal;
        OnHeal.Invoke(amount);
        OnHealEvent.Raise(amount);
    }

    public void TakeDamage(int damage, MonoBehaviour source = null)
    {
        if (damage <= 0)
        {
            return;
        }
        
        currentHealth -= damage;
        OnDamageTaken.Invoke(damage);
        OnDamageTakenEvent.Raise(damage);
        
        if (currentHealth <= 0 && !IsDead)
        {
            Die(source);
        }
    }

    public void Die(MonoBehaviour source)
    {
        IsDead = true;
        OnDeath.Invoke(source);
        OnDeathEvent.Raise(source);
    }
    
    public void Revive(int? health = null)
    {
        IsDead = false;
        currentHealth = health ?? maxHealth;
        OnRevive.Invoke();
        OnReviveEvent.Raise();
    }
}
