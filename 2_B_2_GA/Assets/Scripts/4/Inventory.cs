using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{

    public List<Item> items = new List<Item>();

    public string Find_item = string.Empty;

    
    // Start is called before the first frame update
    void Start()
    {
        items.Add(new Item("sword"));
        items.Add(new Item("shield"));
        items.Add(new Item("potion"));

        Item found = FindItem(Find_item);

        if(found != null)
        {
            Debug.Log("ã�� ������: " + found.itemName);
        }
        else
        {
            Debug.Log("�������� ã�� �� �����ϴ�.");
        }
    }

    public Item FindItem(string _itemName)
    {
        foreach (var item in items)
        {
            if(item.itemName == _itemName)
                return item;
        }
        return null;
    }
    
}
