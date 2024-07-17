using UnityEngine;

// TODO: Look into Unity's Input System
// https://docs.unity3d.com/ScriptReference/Input.html
// You can define axis/buttons in the Input Manager
// https://docs.unity3d.com/Manual/class-InputManager.html
public class Controller {
    // TODO: Use Input.GetAxis for movement
    public ControllerKey Up =  new(KeyCode.W);
    public ControllerKey Left =  new(KeyCode.A);
    public ControllerKey Down =  new(KeyCode.S);
    public ControllerKey Right =  new(KeyCode.D);
    // TODO: Use Input.GetButtonDown for action
    // Input.GetButton_Down_ returns true only in the _first frame_ it is pressed
    public ControllerKey MainAction = new(KeyCode.Space);

    public bool IsPressingMovementKeys() {
        return Up.IsPressed() || Left.IsPressed() || Down.IsPressed() || Right.IsPressed();
    }
}

public class ControllerKey {
    public KeyCode Key;

    public ControllerKey(KeyCode key) {
        Key = key;
    }
    public bool IsPressed() {
        return Input.GetKey(Key);
    }
}