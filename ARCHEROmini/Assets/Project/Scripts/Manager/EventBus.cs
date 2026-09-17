using System;
public interface IEvent { }

public class ResetGameEvent : IEvent
{
    
}

public static class EventBus<T> where T : IEvent
{
    private static Action<T> _onEvent;

    public static void Add(Action<T> listener)
    {
        _onEvent += listener;
    }

    public static void Remove(Action<T> listener)
    {
        _onEvent -= listener;
    }

    public static void Call(T eventData)
    {
        _onEvent?.Invoke(eventData);
    }
}