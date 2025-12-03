using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASterManager : MonoBehaviour
{
    public MapGenerator generator;
    public MapVisualizer visualizer;
    public finderASter finderASter;

    public GameObject pathMarker;
    public GameObject playerPrefab;

    private int[,] map;
    private Vector2Int start;
    private Vector2Int goal;

    void Start()
    {
        map = generator.GenerateValidMap();
        visualizer.Render(map);

        start = new Vector2Int(1, 1);
        goal = new Vector2Int(map.GetLength(1) - 2, map.GetLength(0) - 2);

        if (playerPrefab != null)
            Instantiate(playerPrefab, new Vector3(start.x, 0.5f, start.y), Quaternion.identity);
    }

    // 버튼에서 호출
    public void ShowAStarPath()
    {
        List<Vector2Int> path = finderASter.AStar(map, start, goal);

        if (path == null)
        {
            Debug.Log("경로 없음!");
            return;
        }

        foreach (var p in path)
            Instantiate(pathMarker, new Vector3(p.x, 0.5f, p.y), Quaternion.identity);
    }
}
