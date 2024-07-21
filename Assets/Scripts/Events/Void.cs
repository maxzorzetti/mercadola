using System;
using UnityEngine.Events;

public struct Void { }

[Serializable]
public class UnityVoidEvent : UnityEvent<Void> { } // Wrapper for UnityEvent<Void>

