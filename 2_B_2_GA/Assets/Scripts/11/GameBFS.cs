using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBFS : MonoBehaviour
{
    public MazeGenerator_Probability mazeGenerator;
    public GameObject pathCubePrefab;     // 초록색 큐브
    public GameObject moverCube;          // 움직일 큐브
    public float moveSpeed = 3f;

    int[,] map;
    Vector2Int start = new Vector2Int(1, 1);
    Vector2Int goal;

    bool[,] visited;
    Vector2Int?[,] parent;

    List<Vector2Int> currentPath;

    Vector2Int[] dirs = {
        new Vector2Int(1,0),
        new Vector2Int(-1,0),
        new Vector2Int(0,1),
        new Vector2Int(0,-1)
    };


    void Start()
    {
        // 1) 맵 생성
        map = mazeGenerator.GenerateRandomMap();
        mazeGenerator.DrawMap(map);

        goal = new Vector2Int(mazeGenerator.width - 2, mazeGenerator.height - 2);

        // 2) BFS로 경로 찾기
        currentPath = FindPathBFS();

        // 3) 경로 위에 초록큐브 표시
        DrawPath(currentPath);

        // moverCube 시작 위치 설정
        moverCube.transform.position = new Vector3(start.x, 0.5f, start.y);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentPath != null)
                StartCoroutine(MoveAlongPath(currentPath));
        }
    }

    // ---------------- BFS -----------------
    List<Vector2Int> FindPathBFS()
    {
        int w = map.GetLength(0);
        int h = map.GetLength(1);
        visited = new bool[w, h];
        parent = new Vector2Int?[w, h];

        Queue<Vector2Int> q = new Queue<Vector2Int>();
        q.Enqueue(start);
        visited[start.x, start.y] = true;

        while (q.Count > 0)
        {
            Vector2Int cur = q.Dequeue();

            if (cur == goal)
                return ReconstructPath();

            foreach (var d in dirs)
            {
                int nx = cur.x + d.x;
                int ny = cur.y + d.y;

                if (!InBounds(nx, ny)) continue;
                if (map[nx, ny] == 1) continue;
                if (visited[nx, ny]) continue;

                visited[nx, ny] = true;
                parent[nx, ny] = cur;
                q.Enqueue(new Vector2Int(nx, ny));
            }
        }
        return null;
    }

    bool InBounds(int x, int y)
    {
        return x >= 0 && y >= 0 &&
                x < map.GetLength(0) &&
                y < map.GetLength(1);
    }

    List<Vector2Int> ReconstructPath()
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int? cur = goal;

        while (cur.HasValue)
        {
            path.Add(cur.Value);
            cur = parent[cur.Value.x, cur.Value.y];
        }

        path.Reverse();
        return path;
    }
    // ----------------------------------------


    // 경로 표시 (초록 큐브 생성)
    void DrawPath(List<Vector2Int> path)
    {
        foreach (var p in path)
        {
            Instantiate(pathCubePrefab,
                new Vector3(p.x, 0.2f, p.y),
                Quaternion.identity,
                transform);
        }
    }

    // 큐브 이동
    IEnumerator MoveAlongPath(List<Vector2Int> path)
    {
        foreach (var p in path)
        {
            Vector3 targetPos = new Vector3(p.x, 0.5f, p.y);

            while (Vector3.Distance(moverCube.transform.position, targetPos) > 0.01f)
            {
                moverCube.transform.position =
                    Vector3.MoveTowards(moverCube.transform.position,
                                        targetPos,
                                        moveSpeed * Time.deltaTime);

                yield return null;
            }
        }
    }
}
