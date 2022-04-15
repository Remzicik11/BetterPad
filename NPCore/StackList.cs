using System.Collections;
using System.Collections.Generic;

public class StackList<T>  : ICollection<T>
{
    public List<(T Value, bool Changed)> Items;

    public StackList(int Length) { Items = new List<(T Value, bool Changed)>(Length); }
    public StackList() { Items = new List<(T Value, bool Changed)>(); }

    public int Count => Items.Count;

    public bool IsReadOnly => false;

    public IEnumerator<T> GetChanged()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            var Item = Items[i];

            if (Item.Changed)
            {
                yield return Item.Value;
            }
        }
    }

    public void SetStatus(int index, bool Value)
    {
        var Item = Items[index];
        Item.Changed = Value;
    }

    public void SetStatus(bool Value)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            var Item = Items[i];
            Item.Changed = Value;
        }
    }

    public string GetStringContent()
    {
        string result = "";

        for (int i = 0; i < Items.Count; i++)
        {
            result += "[" + Items[i].Value + ", " + Items[i].Changed + "]" + (i == Items.Count - 1 ? "\n" : "");
        }

        return result;
    }

    public void Add(T Item, bool Changed = false) => Items.Add((Item, Changed));

    public void Add(T Item) => Items.Add((Item, false));

    public T this[int index]
    {
        get => Items[index].Value;

        set
        {
            Items[index] = (value, true);
        }
    }


    public int? IndexOf(T Item)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].Value.Equals(Item))
            {
                return i;
            }
        }

        throw null;
    }

    public void Insert(int index, T Item) => Items.Insert(index, (Item, false));

    public void RemoveAt(int index) => Items.RemoveAt(index);

    public void Clear() => Items.Clear();

    public bool Contains(T Item)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].Value.Equals(Item))
            {
                return true;
            }
        }

        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        array = new T[Items.Count];


        for (int i = arrayIndex; i < Items.Count; i++)
        {
            array[i] = Items[i].Value;
        }
    }

    public bool Remove(T Item)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].Value.Equals(Item))
            {
                Items.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return null;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return null;
    }
}
