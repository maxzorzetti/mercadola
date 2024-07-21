using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerWalk playerWalk;
    PlayerAction playerAction;
    InputAction moveAction;
    InputAction investigateAction;

    void Start()
    {
        playerWalk = GetComponent<PlayerWalk>();
        playerAction = GetComponent<PlayerAction>();
        moveAction = GetComponent<PlayerInput>().actions["Move"];
        investigateAction = GetComponent<PlayerInput>().actions["Investigate"];
    }

    void Update()
    {
        Move();
        Investigate();
    }

    public void Move() {
        var direction = moveAction.ReadValue<Vector2>();
        playerWalk.Move(direction);
    }

    public void Investigate() {
        playerAction.isInvestigating = investigateAction.IsPressed();
    }

    public void HandleInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        playerAction.Interact();
    }
    
    public void HandleInvestigate(InputAction.CallbackContext context)
    {
        if (context.canceled) { playerAction.isInvestigating = false; }
        if (!context.performed) return;
        
        playerAction.Investigate();
    }
    
    public void HandleAmaze(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        playerAction.Amaze();
    }
}
