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

    public UnityEvent<object> OnDeath;
    public UnityEvent OnRevive;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = fullHealthStart ? maxHealth : currentHealth;
    }

    public void TakeDamage(int damage, object source = null)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0 && !IsDead)
        {
            Die(source);
        }
    }

    public void Die(object source)
    {
        IsDead = true;
        OnDeath.Invoke(source);
    }
    
    public void Revive(int? health = null)
    {
        IsDead = false;
        currentHealth = health ?? maxHealth;
        OnRevive.Invoke();
    }
}
