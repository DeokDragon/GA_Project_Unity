using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CountDown(int n)
    {
        if (n==0) return;
        Debug.Log(n);
        CountDown(n-1);
    }
}
