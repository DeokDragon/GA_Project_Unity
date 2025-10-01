using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueSample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Queue<string> queue = new Queue<string>();

        queue.Enqueue("첫 번째");
        queue.Enqueue("두 번째");
        queue.Enqueue("세 번째");

        Debug.Log("====== Queue 1 ======");
        foreach(string Item in queue)
            Debug.Log(Item);
        Debug.Log("=======================");

        Debug.Log("Peek: " + queue.Peek());

        Debug.Log("Dequeue: " + queue.Dequeue());
        Debug.Log("Dequeue: " + queue.Dequeue());

        Debug.Log("남은 데이터 수: " + queue.Count);

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
