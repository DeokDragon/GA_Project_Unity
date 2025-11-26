using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T>
{
    private List<(T item, int priority)> heap = new();

    public int Count => heap.Count;

    private int Parent(int i) => (i - 1) / 2;
    private int Left(int i) => (2 * i) + 1;
    private int Right(int i) => (2 * i) + 2;

    public void Enqueue(T item, int priority)
    {
        heap.Add((item, priority));
        HeapifyUp(heap.Count - 1);
    }

    // ★★ 여기! Pathfinder가 요구하는 메서드 오버로드 ★★
    public bool TryDequeue(out T item, out int priority)
    {
        if (heap.Count == 0)
        {
            item = default;
            priority = 0;
            return false;
        }

        (item, priority) = heap[0];

        // 마지막 원소를 루트로 가져와서 아래로 내려간다
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);

        if (heap.Count > 0)
            HeapifyDown(0);

        return true;
    }

    private void HeapifyUp(int i)
    {
        while (i > 0 && heap[i].priority < heap[Parent(i)].priority)
        {
            Swap(i, Parent(i));
            i = Parent(i);
        }
    }

    private void HeapifyDown(int i)
    {
        while (true)
        {
            int smallest = i;
            int left = Left(i);
            int right = Right(i);

            if (left < heap.Count && heap[left].priority < heap[smallest].priority)
                smallest = left;

            if (right < heap.Count && heap[right].priority < heap[smallest].priority)
                smallest = right;

            if (smallest == i) break;

            Swap(i, smallest);
            i = smallest;
        }
    }

    private void Swap(int a, int b)
    {
        var temp = heap[a];
        heap[a] = heap[b];
        heap[b] = temp;
    }
}
