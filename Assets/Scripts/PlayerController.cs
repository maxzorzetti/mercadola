using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerWalk playerWalk;
    PlayerAction playerAction;
    InputAction moveAction;

    void Start()
    {
        playerWalk = GetComponent<PlayerWalk>();
        playerAction = GetComponent<PlayerAction>();
        moveAction = GetComponent<PlayerInput>().actions["Move"];
    }

    void Update()
    {
        var direction = moveAction.ReadValue<Vector2>();
        playerWalk.Move(direction);
    }

    public void HandleInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        playerAction.Interact();
    }
    
    public void HandleInvestigate(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        playerAction.Investigate();
    }
    
    public void HandleAmaze(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        playerAction.Amaze();
    }
}
