using UnityEngine;

[CreateAssetMenu(fileName = "NewVoidEvent", menuName = "Mercadola/New Void Event")]
public class VoidEvent : BaseEvent<Void>
{
    public void Raise() => Raise(new Void());
}
