using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BruteForce
{
    public static (Dictionary<string, int>, int) Solve(List<expMaterial> m, int targetExp)
    {
        int maxCount = targetExp / 3 + 5;

        int bestCost = int.MaxValue;
        Dictionary<string, int> bestList = null;

        for (int a = 0; a <= maxCount; a++)
            for (int b = 0; b <= maxCount; b++)
                for (int c = 0; c <= maxCount; c++)
                    for (int d = 0; d <= maxCount; d++)
                    {
                        int exp = a * m[0].exp + b * m[1].exp + c * m[2].exp + d * m[3].exp;
                        if (exp < targetExp) continue;

                        int cost = a * m[0].cost + b * m[1].cost + c * m[2].cost + d * m[3].cost;

                        if (cost < bestCost)
                        {
                            bestCost = cost;
                            bestList = new Dictionary<string, int>()
                {
                    { m[0].name, a },
                    { m[1].name, b },
                    { m[2].name, c },
                    { m[3].name, d }
                };
                        }
                    }

        return (bestList, bestCost);
    }
}
