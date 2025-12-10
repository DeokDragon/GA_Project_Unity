using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MergedGreedyAlgorithms
{
    private static (Dictionary<string, int>, int) RunGreedy(
        List<expMaterial> materials,
        int targetExp,
        System.Comparison<expMaterial> sortRule)
    {
        List<expMaterial> m = new(materials);
        m.Sort(sortRule);

        Dictionary<string, int> result = new();
        int totalExp = 0;
        int totalCost = 0;

        foreach (var mat in m)
        {
            while (totalExp < targetExp)
            {
                totalExp += mat.exp;
                totalCost += mat.cost;

                if (!result.ContainsKey(mat.name))
                    result[mat.name] = 0;

                result[mat.name]++;
            }
        }

        return (result, totalCost);
    }

    public static (Dictionary<string, int>, int) LeastWaste(List<expMaterial> m, int targetExp)
    {
        return RunGreedy(m, targetExp, (a, b) => a.exp.CompareTo(b.exp));
    }

    public static (Dictionary<string, int>, int) GoldEfficiency(List<expMaterial> m, int targetExp)
    {
        return RunGreedy(m, targetExp,
            (a, b) => (b.exp / (float)b.cost).CompareTo(a.exp / (float)a.cost));
    }

    public static (Dictionary<string, int>, int) HighExp(List<expMaterial> m, int targetExp)
    {
        return RunGreedy(m, targetExp,
            (a, b) => b.exp.CompareTo(a.exp));
    }
}
