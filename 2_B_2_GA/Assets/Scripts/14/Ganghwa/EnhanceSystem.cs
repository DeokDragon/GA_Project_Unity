using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhanceSystem : MonoBehaviour
{
    public int currentLevel = 1;

    public List<expMaterial> materials;

    void Awake()
    {
        materials = new List<expMaterial>()
        {
            new expMaterial("Mini", 3, 8),
            new expMaterial("middle", 5, 12),
            new expMaterial("big", 12, 30),
            new expMaterial("many big", 20, 45)
        };
    }

    public int GetRequiredExp(int level)
    {
        return 8 * level * level;
    }

    public void LevelUp()
    {
        currentLevel++;
    }
}
