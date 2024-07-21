using UnityEngine;

public class NPC : MonoBehaviour
{
    public string Name;

    public string Greetings;

    public void Greet()
    {
        Debug.Log($"{Name}: {Greetings}");
    }
    
    public void Speak(string message)
    {
        Debug.Log($"{Name}: {message}");
    }

    public void Flip()
    {
        var sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.flipX = !sprite.flipX;
    }

    public void HandleDialogueStart(DialogueEventData data)
    {
        if (data.Dialogue.speaker != Name && !data.IsDialogueEnd) return;
     
        Greet();
    }
}
