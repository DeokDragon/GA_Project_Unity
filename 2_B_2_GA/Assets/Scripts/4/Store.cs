using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField searchInput;      // �˻�â
    public Transform itemListParent;        // ScrollView Content
    public GameObject itemPrefab;           // Item Prefab

    private List<Item> allItems = new List<Item>();
    private List<Item> sortedItems = new List<Item>();

    void Start()
    {
        // 1. Item_00 ~ Item_99 ����
        for (int i = 0; i < 100; i++)
        {
            Item item = new Item($"Item_{i:D2}", Random.Range(1, 10));
            allItems.Add(item);
        }

        // 2. ���ĵ� ����Ʈ ���� (Binary Search��)
        sortedItems = new List<Item>(allItems);
        sortedItems.Sort((a, b) => a.itemName.CompareTo(b.itemName));

        // 3. ScrollView�� ��ü ������ ǥ��
        DisplayItems(allItems);
    }

    // Linear Search ��ư Ŭ��
    public void OnLinearSearch()
    {
        Debug.Log("Linear Search Clicked!");
        string keyword = searchInput.text;
        Debug.Log($"Searching for: {keyword}");

        List<Item> result = new List<Item>();
        foreach (Item item in allItems)
        {
            if (item.itemName.Contains(keyword))
                result.Add(item);
        }

        DisplayItems(result);
    }

    // Binary Search ��ư Ŭ��
    public void OnBinarySearch()
    {
        Debug.Log("Binary Search Clicked!");
        string keyword = searchInput.text;
        Debug.Log($"Searching for: {keyword}");

        List<Item> result = new List<Item>();

        int left = 0, right = sortedItems.Count - 1;
        while (left <= right)
        {
            int mid = (left + right) / 2;
            int cmp = sortedItems[mid].itemName.CompareTo(keyword);

            if (cmp == 0)
            {
                result.Add(sortedItems[mid]);
                // ���� �̸� �¿� Ž��
                int l = mid - 1;
                while (l >= 0 && sortedItems[l].itemName == keyword) { result.Add(sortedItems[l]); l--; }
                int r = mid + 1;
                while (r < sortedItems.Count && sortedItems[r].itemName == keyword) { result.Add(sortedItems[r]); r++; }
                break;
            }
            else if (cmp < 0) left = mid + 1;
            else right = mid - 1;
        }

        DisplayItems(result);
    }

    // ScrollView�� ������ ǥ��
    private void DisplayItems(List<Item> items)
    {
        foreach (Transform child in itemListParent)
            Destroy(child.gameObject);

        foreach (Item item in items)
        {
            GameObject go = Instantiate(itemPrefab, itemListParent);
            TMP_Text text = go.GetComponentInChildren<TMP_Text>();
            if (text != null)
                text.text = $"{item.itemName}";
            else
                Debug.LogWarning("TMP_Text not found in prefab!");
        }
    }
}