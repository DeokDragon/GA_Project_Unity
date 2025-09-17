using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;


public class SelectionSortTest : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    public void Onclick()
    {
        Stopwatch sw2 = new Stopwatch();
        sw2.Reset();
        sw2.Start();
        int[] data = GenerateRandomArray(10000);
        
        
        //Debug.Log("Á¤·Ä Áß...");
        StartSelectionSort(data);
        foreach(var item in data)
        {
            //Debug.Log(item);
        }
        sw2.Stop();
        long selectionTime2 = sw2.ElapsedMilliseconds;
        UnityEngine.Debug.Log($"SelectionSort : {selectionTime2} ms");
        text.text = $"SelectionSort : {selectionTime2} ms";



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

    public static void StartSelectionSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (arr[j] < arr[minIndex])
                {
                    minIndex = j;
                }
            }

            int temp = arr[minIndex];
            arr[minIndex] = arr[i];
            arr[i] = temp;
        }
    }

}
