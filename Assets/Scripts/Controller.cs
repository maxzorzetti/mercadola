using UnityEngine;

public class Controller {
    public ControllerKey Up =  new(KeyCode.W);
    public ControllerKey Left =  new(KeyCode.A);
    public ControllerKey Down =  new(KeyCode.S);
    public ControllerKey Right =  new(KeyCode.D);

    public bool IsPressingAnyKeys() {
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