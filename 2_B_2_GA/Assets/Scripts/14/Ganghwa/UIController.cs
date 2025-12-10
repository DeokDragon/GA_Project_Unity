using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{
    public EnhanceSystem system;

    public TextMeshProUGUI resultText;
    public TextMeshProUGUI levelInfoText;

    // 강화 버튼에서 사용할 계산 결과 저장
    private Dictionary<string, int> lastCalculatedMaterials;
    private int lastCalculatedCost = 0;
    private int lastNeedExp = 0;

    void Start()
    {
        UpdateLevelInfo();
    }

    
    public void OnBruteForce()
    {
        RunCalculation((m, exp) => BruteForce.Solve(m, exp));
    }

    public void OnLeastWaste()
    {
        RunCalculation((m, exp) => MergedGreedyAlgorithms.LeastWaste(m, exp));
    }

    public void OnEfficiency()
    {
        RunCalculation((m, exp) => MergedGreedyAlgorithms.GoldEfficiency(m, exp));
    }

    public void OnHighExp()
    {
        RunCalculation((m, exp) => MergedGreedyAlgorithms.HighExp(m, exp));
    }

    private void RunCalculation(
        System.Func<List<expMaterial>, int, (Dictionary<string, int>, int)> algorithm)
    {
        int need = system.GetRequiredExp(system.currentLevel);

        var (list, cost) = algorithm(system.materials, need);


        lastNeedExp = need;
        lastCalculatedMaterials = list;
        lastCalculatedCost = cost;


        ShowResult(list, cost, need);
    }


    public void OnEnhance()
    {
        if (lastCalculatedMaterials == null)
        {
            resultText.text = "먼저 하나의 계산 기능을 사용하세요!";
            return;
        }

        system.LevelUp();

        UpdateLevelInfo();

        resultText.text =
            $"강화 성공!\n" +
            $"다음 레벨까지 필요한 경험치가 갱신되었습니다.\n\n" +
            $"사용한 재료:\n";

        foreach (var kv in lastCalculatedMaterials)
        {
            if (kv.Value > 0)
                resultText.text += $"{kv.Key} x {kv.Value}\n";
        }

        resultText.text += $"\n총 비용: {lastCalculatedCost} gold";

        lastCalculatedMaterials = null;
    }

    private void UpdateLevelInfo()
    {
        int nextLevel = system.currentLevel + 1;
        int need = system.GetRequiredExp(system.currentLevel);

        levelInfoText.text =
            $"현재 레벨: {system.currentLevel} → {nextLevel}\n" +
            $"필요 경험치: {need}";
    }

    private void ShowResult(Dictionary<string, int> list, int cost, int need)
    {
        string s = $"필요 경험치: {need}\n\n";

        foreach (var kv in list)
        {
            if (kv.Value > 0)
                s += $"{kv.Key} x {kv.Value}\n";
        }

        s += $"\n총 가격: {cost} gold";
        resultText.text = s;
    }
}
