using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent interaction;
    
    public void Interact()
    {
        interaction?.Invoke();
    }
}
