using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSortTest : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    public void Onclick()
    {
        Stopwatch sw1 = new Stopwatch();
        sw1.Reset();
        sw1.Start();
        int[] data = GenerateRandomArray(10000);

        //UnityEngine.Debug.Log("Á¤·Ä Áß...");
        StartBubbleSort(data);
        foreach (var item in data)
        {
            //Debug.Log(item);
        }
        sw1.Stop();
        long selectionTime1 = sw1.ElapsedMilliseconds;
        UnityEngine.Debug.Log($"BubbleSort : {selectionTime1} ms");
        text.text = $"BubbleSort : {selectionTime1} ms";

    }

    int[] GenerateRandomArray(int size)
    {
        int[] arr = new int[size];
        System.Random rand = new System.Random();
        for (int i = 0; i < size; i++)
        {
            arr[i] = Random.Range(0, 1000000);
        }
        return arr;
    }

    public static void StartBubbleSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            bool swapped = false;
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                    swapped = true;
                }
            }
            if (!swapped) break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
