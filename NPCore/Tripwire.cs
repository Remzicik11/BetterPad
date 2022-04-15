using System;

public struct Tripwire<T>
{
    public T Value
    {
        get
        {
            return InstanceValue;
        }
        set
        {
            InstanceValue = value;

            if (Triggered != null) { Triggered.Invoke(); }
        }
    }

    public Action Triggered;

    private T InstanceValue;
}