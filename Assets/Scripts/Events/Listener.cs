public interface Listener<T>
{
    public void OnEventRaised(BaseEvent<T> raisedEvent, T value);
}