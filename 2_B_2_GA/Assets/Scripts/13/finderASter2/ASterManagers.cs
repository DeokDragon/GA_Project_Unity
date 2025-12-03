using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASterManagers : MonoBehaviour
{
    public MapGenerator generator;
    public MapVisualizer visualizer;
    public finderASters finderASter;
    public EnermyManager enemyManager;

    public GameObject pathMarker;
    public GameObject playerPrefab;

    private int[,] map;
    private Vector2Int start;
    private Vector2Int goal;
    private List<Vector2Int> enemies;

    void Start()
    {
        map = generator.GenerateValidMap();
        visualizer.Render(map);

        enemies = enemyManager.SpawnEnemies(map);

        start = new Vector2Int(1, 1);
        goal = new Vector2Int(map.GetLength(1) - 2, map.GetLength(0) - 2);

        if (playerPrefab != null)
            Instantiate(playerPrefab, new Vector3(start.x, 0.5f, start.y), Quaternion.identity);
    }

    public void ShowAStarPath()
    {
        List<Vector2Int> path = finderASter.AStar(map, start, goal, enemies);

        if (path == null)
        {
            Debug.Log("경로 없음!");
            return;
        }

        foreach (var p in path)
            Instantiate(pathMarker, new Vector3(p.x, 0.5f, p.y), Quaternion.identity);
    }
}
