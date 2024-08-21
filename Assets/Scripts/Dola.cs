using UnityEngine;

public class Dola : MonoBehaviour
{
    public void Speak(string message)
    {
        Debug.Log($"{name}: {message}");
    }

    public void Die()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }
}