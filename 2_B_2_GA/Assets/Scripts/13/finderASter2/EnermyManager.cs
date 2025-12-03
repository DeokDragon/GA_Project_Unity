using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 6;

    public List<Vector2Int> SpawnEnemies(int[,] map)
    {
        int h = map.GetLength(0);
        int w = map.GetLength(1);

        List<Vector2Int> enemies = new List<Vector2Int>();

        while (enemies.Count < enemyCount)
        {
            int x = Random.Range(1, w - 1);
            int y = Random.Range(1, h - 1);

            if (map[y, x] != 0)
            {
                enemies.Add(new Vector2Int(x, y));
                Instantiate(enemyPrefab, new Vector3(x, 0.5f, y), Quaternion.identity);
            }
        }

        return enemies;
    }
}
