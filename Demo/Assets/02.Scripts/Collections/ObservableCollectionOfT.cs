using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservableCollection<T> : IEnumerable<T>
{
    public T this[int index]
    {
        get => items[index];
        set => Change(index, value);
    }

    public List<T> items => new List<T>();

    public event Action<int, T> onItemChanged;
    public event Action<int, T> onItemAdded;
    public event Action<int, T> onItemRemoved;
    public event Action onCollectionChanged;

    public void Change(int index, T item)
    {
        items[index] = item;
        onItemChanged?.Invoke(index, item);
        onCollectionChanged?.Invoke();
    }
    
    public void Swap(int index1, int index2)
    {
        if (index1 >= items.Count || index1 < 0 ||
            index2 >= items.Count || index2 < 0)
            throw new IndexOutOfRangeException();

        T item1 = items[index1];
        T item2 = items[index2];
        items[index2] = item1;
        items[index1] = item2;
        onItemChanged?.Invoke(index1, item2);
        onItemChanged?.Invoke(index2, item1);
        onCollectionChanged?.Invoke();
    }

    public void Add(T item)
    {
        items.Add(item);
        onItemAdded?.Invoke(items.Count - 1, item);
        onCollectionChanged?.Invoke();
    }

    public void RemoveAt(int index)
    {
        T tmp = items[index];
        items.RemoveAt(index);
        onItemRemoved?.Invoke(index, tmp);
        onCollectionChanged?.Invoke();
    }

    public bool Remove(T item)
    {
        int index = items.IndexOf(item);
        if (index < 0)
            return false;

        RemoveAt(index);
        return true;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return items.GetEnumerator();
    }
}
