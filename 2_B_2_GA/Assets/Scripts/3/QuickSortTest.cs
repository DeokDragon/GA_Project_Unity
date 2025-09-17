using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class QuickSortTest : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    public void Onclick()
    {
        Stopwatch sw3 = new Stopwatch();
        sw3.Reset();
        sw3.Start();
        int[] data = GenerateRandomArray(10000);

        //Debug.Log("Á¤·Ä Áß...");
        StartQuickSort(data, 0, data.Length - 1);
        foreach (var item in data)
        {
            //Debug.Log(item);
        }
        sw3.Stop();
        long selectionTime3 = sw3.ElapsedMilliseconds;
        UnityEngine.Debug.Log($"QuickSort : {selectionTime3} ms");
        text.text = $"QuickSort : {selectionTime3} ms";

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

    public static void StartQuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(arr, low, high);
            StartQuickSort(arr, low, pivotIndex - 1);
            StartQuickSort(arr, pivotIndex + 1, high);
        }
    }

    private static int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = (low - 1);
        for (int j = low; j < high; j++)
        {
            if (arr[j] <= pivot)
            {
                i++;
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
        int temp2 = arr[i + 1];
        arr[i +1] = arr[high];
        arr[high] = temp2;

        return i + 1;
    }
}
