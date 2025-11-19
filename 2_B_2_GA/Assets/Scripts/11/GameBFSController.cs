using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBFSController : MonoBehaviour
{
    [Header("Maze & Prefabs")]
    public MazeGenerator_Probability mazeGenerator;
    public GameObject pathCubePrefab;    // 초록색 경로 큐브
    public GameObject moverCubePrefab;   // 움직일 큐브

    [Header("Settings")]
    public float moveSpeed = 3f;
    [Range(0f, 1f)] public float wallProbability = 0.3f;

    private int[,] map;
    private Vector2Int start = new Vector2Int(1, 1);
    private Vector2Int goal;
    private bool[,] visited;
    private Vector2Int?[,] parent;
    private List<Vector2Int> currentPath;
    private Vector2Int[] dirs = { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1) };
    private GameObject moverCube;

    void Start()
    {
        GenerateAndDrawMap();
    }

    // ---------------- 맵 생성 ----------------
    void GenerateAndDrawMap()
    {
        mazeGenerator.wallProbability = wallProbability;
        map = mazeGenerator.GenerateRandomMap();
        mazeGenerator.DrawMap(map);

        goal = new Vector2Int(mazeGenerator.width - 2, mazeGenerator.height - 2);

        // moverCube 생성
        if (moverCube != null) Destroy(moverCube);
        moverCube = Instantiate(moverCubePrefab, new Vector3(start.x, 1f, start.y), Quaternion.identity);

        // BFS로 경로 찾기
        currentPath = FindPathBFS();

        // 경로 표시
        DrawPath(currentPath);

        // 가장 먼 칸 표시
        DrawFarthestCells();
    }

    // ---------------- BFS ----------------
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

        // goal까지 경로 재구성
        if (!visited[goal.x, goal.y]) return null;
        return ReconstructPath(goal);
    }

    bool InBounds(int x, int y)
    {
        return x >= 0 && y >= 0 && x < map.GetLength(0) && y < map.GetLength(1);
    }

    List<Vector2Int> ReconstructPath(Vector2Int end)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int? cur = end;
        while (cur.HasValue)
        {
            path.Add(cur.Value);
            cur = parent[cur.Value.x, cur.Value.y];
        }
        path.Reverse();
        return path;
    }

    // ---------------- 경로 표시 ----------------
    void DrawPath(List<Vector2Int> path)
    {
        if (path == null) return;

        foreach (var p in path)
        {
            Instantiate(pathCubePrefab, new Vector3(p.x, 0.2f, p.y), Quaternion.identity, transform);
        }
    }

    // ---------------- 가장 먼 칸 표시 ----------------
    void DrawFarthestCells()
    {
        List<Vector2Int> farthest = new List<Vector2Int>();
        int maxDist = 0;

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (visited[x, y])
                {
                    int dist = 0;
                    Vector2Int? cur = new Vector2Int(x, y);
                    while (cur.HasValue)
                    {
                        dist++;
                        cur = parent[cur.Value.x, cur.Value.y];
                    }
                    if (dist > maxDist)
                    {
                        maxDist = dist;
                        farthest.Clear();
                        farthest.Add(new Vector2Int(x, y));
                    }
                    else if (dist == maxDist)
                    {
                        farthest.Add(new Vector2Int(x, y));
                    }
                }
            }
        }

        // 빨간 큐브 표시
        foreach (var p in farthest)
        {
            GameObject c = GameObject.CreatePrimitive(PrimitiveType.Cube);
            c.transform.position = new Vector3(p.x, 0.5f, p.y);
            c.GetComponent<Renderer>().material.color = Color.red;
            c.transform.parent = transform;
        }
    }

    // ---------------- 이동 ----------------
    public void MoveAlongPathButton()
    {
        if (currentPath != null)
            StartCoroutine(MoveAlongPath(currentPath));
    }

    IEnumerator MoveAlongPath(List<Vector2Int> path)
    {
        foreach (var p in path)
        {
            // 목표 위치
            Vector3 targetPos = new Vector3(p.x, 0.5f, p.y);

            // 한 칸 이동 (뚝뚝)
            moverCube.transform.position = targetPos;

            // 이동 후 잠시 대기 (눈으로 보기 좋게)
            yield return new WaitForSeconds(0.2f);
        }
    }
}
