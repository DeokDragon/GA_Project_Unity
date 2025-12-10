using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class expMaterial
{
    public string name;   
    public int exp;       
    public int cost;      

    public expMaterial(string name, int exp, int cost)
    {
        this.name = name;
        this.exp = exp;
        this.cost = cost;
    }
}
