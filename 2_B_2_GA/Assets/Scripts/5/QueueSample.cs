using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueSample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Queue<string> queue = new Queue<string>();

        queue.Enqueue("ù ��°");
        queue.Enqueue("�� ��°");
        queue.Enqueue("�� ��°");

        Debug.Log("====== Queue 1 ======");
        foreach(string Item in queue)
            Debug.Log(Item);
        Debug.Log("=======================");

        Debug.Log("Peek: " + queue.Peek());

        Debug.Log("Dequeue: " + queue.Dequeue());
        Debug.Log("Dequeue: " + queue.Dequeue());

        Debug.Log("���� ������ ��: " + queue.Count);

        Debug.Log("====== Queue 2 ======");
        foreach (string Item in queue)
            Debug.Log(Item);
        Debug.Log("=======================");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
