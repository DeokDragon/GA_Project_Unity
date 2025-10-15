using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using static UnityEditor.Progress;

public class PQueueTest : MonoBehaviour
{
    private void Start()
    {
        var queue = new SimplePriorityQueue<string>();
        queue.Enqueue("����", 5);
        queue.Enqueue("�ü�", 10);
        queue.Enqueue("����", 12);
        queue.Enqueue("������", 7);

        while (queue.Count > 0)
        {
            Debug.Log(queue.Dequeue());
        }    
    }
    
}
