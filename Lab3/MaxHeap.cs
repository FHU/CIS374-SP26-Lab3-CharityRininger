using System;

namespace Lab3;

public class MaxHeap<T> where T: IComparable<T>
{
    private T[] array;
    private const int initialSize = 8;

    public int Count { get; private set; }

    public int Capacity => array.Length;

    public bool IsEmpty => Count == 0;


    public MaxHeap(T[] initialArray = null)
    {
        array = new T[initialSize];

        if (initialArray == null) return;

        foreach (var item in initialArray)
        {
            Add(item);
        }

    }

    /// <summary>
    /// Returns the Max item but does NOT remove it.
    /// Time complexity: O( 1 )
    /// </summary>
    public T Peek()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException();
        }

        return array[0];
    }

    // TODO
    /// <summary>
    /// Adds given item to the heap.
    /// Time complexity: O(log(n)) ***BUT*** it might be O(N) if we have to resize
    /// </summary>
    public void Add(T item)
    {
        array[Count] = item;
        TrickleUp(Count);
        Count++;

        if (Count == Capacity)
        {
            DoubleArrayCapacity();
        }
    }

    public T Extract()
    {
        return ExtractMax();
    }

    /// <summary>
    /// Removes and returns the max item in the Max-heap.
    /// Time complexity: O( n )
    /// </summary>
    public T ExtractMin()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException();
        }
        T min = array[0];
        for (int i = 1; i < Count; i++)
        {
            if (array[i].CompareTo(min) < 0)
            {
                min = array[i];
            }
        }
        Remove(min);
        return min;
    }

    // TODO
    /// <summary>
    /// Removes and returns the Max item in the Max-heap.
    /// Time complexity: O( log(n) )
    /// </summary>
    public T ExtractMax()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException();
        }

        // save the Max from the root
        T Max = array[0];

        // swap the Max with the last item
        array[0] = array[Count - 1];

        // remove the "last" item
        Count--;

        // trickle down from root
        TrickleDown(0);

        return Max;
    }

    /// <summary>
    /// Returns true if the heap contains the given value; otherwise false.
    /// Time complexity: O( n )
    /// </summary>
    public bool Contains(T value)
    {
        for (int i = 0; i < Count; i++)
        {
            if (array[i].CompareTo(value) == 0)
            {
                return true;
            }
        }

        return false;
    }

    // TODO
    /// <summary>
    /// Updates the first element with the given value from the heap.
    /// Time complexity: O( n )
    /// </summary>
    public void Update(T oldValue, T newValue)
    {
        if (IsEmpty)
            throw new InvalidOperationException();
        bool found = false;
        // find the node to update - O(n)
        for (int i = 0; i < Count; i++)
        {
            if (array[i].Equals(oldValue))
            {
                // update value - O(1)
                array[i] = newValue;
                found = true;

                // trickle up or trickle down - O( log(n) )
                if(i == 0)
                    TrickleDown(i);
                if (array[i].CompareTo(array[Parent(i)]) < 0)
                {
                    TrickleUp(i);
                }
                if (array[i].CompareTo(array[Parent(i)]) > 0)
                {
                    TrickleDown(i);
                }
                break;
            }
        }
        if (!found)
            throw new InvalidOperationException($"Value {oldValue} not found in heap");
    }

    // TODO
    /// <summary>
    /// Removes the first element with the given value from the heap.
    /// Time complexity: O( n )
    /// </summary>
    public void Remove(T value)
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException();
        }
        // find the node to remove
        for (int i = 0; i < Count; i++)
        {
            if (array[i].Equals(value))
            {
                array[i] = array[Count - 1];
                Count--;
                if (i == 0 || array[i].CompareTo(array[Parent(i)]) < 0)
                {
                    TrickleDown(i);
                }
                if (array[i].CompareTo(array[Parent(i)]) > 0)
                {
                    TrickleUp(i);
                }
                break;
            }
        }
    }

    // TODO
    // Time Complexity: O( log n )
    private void TrickleUp(int index)
    {
        if(index == 0)
            return;
        int parent = Parent(index);
        if (array[index].CompareTo(array[parent]) > 0)
        {
            Swap(index, parent);
            TrickleUp(parent);
        }
    }

    // TODO
    // Time Complexity: O( log n )
private void TrickleDown(int index)
{
    int left = LeftChild(index);
    int right = RightChild(index);

    if (left >= Count)
        return;
    int bigChild = left;

    if (right < Count && array[right].CompareTo(array[left]) > 0)
    {
        bigChild = right;
    }

    if (array[index].CompareTo(array[bigChild]) < 0)
    {
        Swap(index, bigChild);
        TrickleDown(bigChild);
    }
}

    // TODO
    /// <summary>
    /// Gives the position of a node's parent, the node's position in the heap.
    /// </summary>
    private static int Parent(int position)
    {
        return (position - 1) / 2;
    }

    // TODO
    /// <summary>
    /// Returns the position of a node's left child, given the node's position.
    /// </summary>
    private static int LeftChild(int position)
    {
        return 2 * position + 1;
    }

    // TODO
    /// <summary>
    /// Returns the position of a node's right child, given the node's position.
    /// </summary>
    private static int RightChild(int position)
    {
        return 2 * position + 2;
    }

    private void Swap(int index1, int index2)
    {
        var temp = array[index1];

        array[index1] = array[index2];
        array[index2] = temp;
    }

    private void DoubleArrayCapacity()
    {
        Array.Resize(ref array, array.Length * 2);
    }
}