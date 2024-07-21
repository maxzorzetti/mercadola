using UnityEngine;

public class Collector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        collider2D.GetComponent<Collectible>()?.Collect();
    }
}