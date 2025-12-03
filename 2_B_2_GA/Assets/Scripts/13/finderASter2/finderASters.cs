using System.Collections.Generic;
using UnityEngine;

public class finderASters : MonoBehaviour
{
    public List<Vector2Int> AStar(int[,] map, Vector2Int start, Vector2Int goal, List<Vector2Int> enemies)
    {
        int h = map.GetLength(0);
        int w = map.GetLength(1);

        int[,] gCost = new int[h, w];
        bool[,] visited = new bool[h, w];
        Vector2Int?[,] parent = new Vector2Int?[h, w];

        for (int y = 0; y < h; y++)
            for (int x = 0; x < w; x++)
                gCost[y, x] = int.MaxValue;

        gCost[start.y, start.x] = 0;

        List<Vector2Int> open = new List<Vector2Int>();
        open.Add(start);

        Vector2Int[] dirs =
        {
            new Vector2Int(1,0),
            new Vector2Int(-1,0),
            new Vector2Int(0,1),
            new Vector2Int(0,-1)
        };

        while (open.Count > 0)
        {
            int bestIndex = 0;
            int bestF = F(open[0], gCost, goal, enemies);

            for (int i = 1; i < open.Count; i++)
            {
                int f = F(open[i], gCost, goal, enemies);
                if (f < bestF)
                {
                    bestF = f;
                    bestIndex = i;
                }
            }

            Vector2Int cur = open[bestIndex];
            open.RemoveAt(bestIndex);

            if (visited[cur.y, cur.x]) continue;
            visited[cur.y, cur.x] = true;

            if (cur == goal)
                return Reconstruct(parent, start, goal);

            foreach (var d in dirs)
            {
                int nx = cur.x + d.x;
                int ny = cur.y + d.y;

                if (!InBounds(map, ny, nx)) continue;
                if (map[ny, nx] == 0) continue;
                if (visited[ny, nx]) continue;

                int moveCost = TileCost(map[ny, nx]);
                int newG = gCost[cur.y, cur.x] + moveCost;

                if (newG < gCost[ny, nx])
                {
                    gCost[ny, nx] = newG;
                    parent[ny, nx] = cur;

                    Vector2Int nextPos = new Vector2Int(nx, ny);
                    if (!open.Contains(nextPos))
                        open.Add(nextPos);
                }
            }
        }

        return null;
    }

    int F(Vector2Int pos, int[,] gCost, Vector2Int goal, List<Vector2Int> enemies)
    {
        return gCost[pos.y, pos.x] + H(pos, goal, enemies);
    }

    int H(Vector2Int a, Vector2Int b, List<Vector2Int> enemies)
    {
        int manhattan = Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);

        float enemyPenalty = 0f;

        foreach (var e in enemies)
        {
            float dist = Vector2Int.Distance(a, e);
            enemyPenalty += 15f / (dist + 1f);
        }

        return manhattan + Mathf.RoundToInt(enemyPenalty);
    }

    int TileCost(int tile)
    {
        switch (tile)
        {
            case 1: return 1;
            case 2: return 3;
            case 3: return 5;
            default: return int.MaxValue;
        }
    }

    bool InBounds(int[,] map, int y, int x)
    {
        return y >= 0 && x >= 0 &&
               y < map.GetLength(0) &&
               x < map.GetLength(1);
    }

    List<Vector2Int> Reconstruct(Vector2Int?[,] parent, Vector2Int start, Vector2Int goal)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int? cur = goal;

        while (cur != null)
        {
            path.Add(cur.Value);
            if (cur.Value == start) break;
            cur = parent[cur.Value.y, cur.Value.x];
        }

        path.Reverse();
        return path;
    }
}
