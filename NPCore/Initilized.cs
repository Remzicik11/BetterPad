using System;

public struct Initilized<T>
{
    public bool IsInitilzed { get; set; }

    private T value;

    public InitCallback OnInit;

    public T Value
    {
        get
        {
            if (!IsInitilzed)
            {
                if (OnInit == null)
                {
                    throw new NullReferenceException("Can not initilize value, On init is not set");
                }
                IsInitilzed = true;


                var result = value = OnInit.Invoke();
                OnInit = null;
                return result;
            }
            else
            {
                return value;
            }
        }
        set
        {
            this.value = value;
        }
    }

    public delegate T InitCallback();
}