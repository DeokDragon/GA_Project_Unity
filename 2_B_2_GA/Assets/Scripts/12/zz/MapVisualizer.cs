using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVisualizer : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject landPrefab;
    public GameObject forestPrefab;
    public GameObject mudPrefab;

    public void Render(int[,] map)
    {
        int h = map.GetLength(0);
        int w = map.GetLength(1);

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                GameObject prefab = null;

                switch (map[y, x])
                {
                    case 0: prefab = wallPrefab; break;
                    case 1: prefab = landPrefab; break;
                    case 2: prefab = forestPrefab; break;
                    case 3: prefab = mudPrefab; break;
                }

                if (prefab != null)
                    Instantiate(prefab, new Vector3(x, 0, y), Quaternion.identity, transform);
            }
        }
    }
}
