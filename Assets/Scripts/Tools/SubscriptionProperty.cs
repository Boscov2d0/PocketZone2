using System;

public class SubscriptionProperty<T>
{
    private T _value;
    private Action _onChangeValue;
    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            _onChangeValue?.Invoke();
        }

    }
    public void SubscribeOnChange(Action subscriptionAction) =>
        _onChangeValue += subscriptionAction;

    public void UnSubscribeOnChange(Action unSubscriptionAction) =>
            _onChangeValue -= unSubscriptionAction;
}