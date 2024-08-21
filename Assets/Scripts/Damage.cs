using UnityEngine;

public class Damage : MonoBehaviour
{
    public static int DefaultDamage = 1;
    
    [Range(0, 10)]
    public int damage = DefaultDamage;
}