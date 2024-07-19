using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerWalk playerWalk;
    InputAction moveAction;

    void Start()
    {
        playerWalk = GetComponent<PlayerWalk>();
        moveAction = GetComponent<PlayerInput>().actions["Move"];
    }

    void Update()
    {
        var direction = moveAction.ReadValue<Vector2>();
        playerWalk.Move(direction);
    }
}
