using System;

[Serializable]
public class EventResponseTuple<E, U>
{
    public E Event;
    public U Response;
}